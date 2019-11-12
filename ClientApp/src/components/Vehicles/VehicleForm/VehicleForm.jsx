import React, { Component } from 'react';

class VehicleForm extends Component {
    state = { 
        makes: [],
        currentMakeId: '',
        features: [],
        chosenFeatures: []
     }

    render() {       
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
                <div className="form-group">
                   {this.state.features.map(i => {
                       return <label htmlFor={i.id}>
                           <span>{i.name}</span>
                           <input type="checkbox" id={i.id}/>
                       </label>
                   })} 
                </div>
                <button type='submit' class="btn btn-primary">Send</button>
            </form>
        );
    }

    componentDidMount() {
        this.fetchData()
    }

    fetchData = async () => {
        const makesResp = await fetch('api/makes');
        const featuresResp = await fetch('api/features');
        const makes = await makesResp.json(); 
        const features = await featuresResp.json();
        
        this.setState({
            makes,
            features,
            currentMakeId: makes[0].id.toString()
        })   
    }

    handleMakeChange = (event) => {
        this.setState({
            currentMakeId: event.target.value
        })
    }
}
 
export {VehicleForm};