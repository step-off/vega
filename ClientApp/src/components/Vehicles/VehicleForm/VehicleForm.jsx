import React, { Component } from 'react';

class VehicleForm extends Component {
    state = { 
        makes: [],
        currentMakeId: ''
     }

    render() { 
        console.log(this.state.currentMakeId);
        console.log(this.state.makes);
        
        
        return ( 
            <form>
                <div className="form-group">
                    <label htmlFor="make">Make</label>
                    <select className="form-control" id="make" onChange={this.handleMakeChange}>
                        {this.state.makes.map(make => (
                            <option value={make.id} key={make.id}>{make.name}</option>
                        ))}
                    </select>
                </div>
                <div className="form-group">
                    <label htmlFor="model">Model</label>
                    <select className="form-control" id="model">
                        {this.state.currentMakeId ? (
                            this.state.makes.find(i => i.id.toString() === this.state.currentMakeId).models.map(j => (
                                <option value={j.id}>{j.name}</option>
                            ))
                        ) : null}
                    </select>
                </div>
            </form>
        );
    }

    componentDidMount() {
        this.fetchData()
    }

    fetchData = async () => {
        const resp = await fetch('api/makes');
        const result = await resp.json(); 
        this.setState({
            makes: result,
            currentMakeId: result[0].id.toString()
        })   
    }

    handleMakeChange = (event) => {
        this.setState({
            currentMakeId: event.target.value
        })
    }
}
 
export {VehicleForm};