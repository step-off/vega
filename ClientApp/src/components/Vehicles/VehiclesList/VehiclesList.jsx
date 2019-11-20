import React, { Component } from 'react';
import VehicleItem from './VehicleItem';

const DEFAULT_SELECETED_MAKE_ID = -1;

class VehiclesList extends Component {
    state = { 
        vehicles: [],
        makes: [],
        selectedMakeId: DEFAULT_SELECETED_MAKE_ID
     }
    render() { 
        return <div>
            <div className='form-group'>
                <label htmlFor="make">Choose make name</label>
                <select id="make" className='form-control' onChange={this.handleMakeChange}>
                    <option value="" selected={this.state.selectedMakeId === DEFAULT_SELECETED_MAKE_ID}>Not selected</option>
                    {this.state.makes.map(i => {
                        return <option key={i.id} value={i.id} selected={this.state.selectedMakeId === i.id}>{i.name}</option>
                    })}
                </select>
            </div>
            <table>
            <thead>
                <tr>
                    <td>Id</td>
                    <td>Name</td>
                </tr>
            </thead>
            <tbody>
                {this.state.vehicles.map(i => <VehicleItem key={i.id} vehicle={i}/>)}
            </tbody>
        </table>
        </div>
    }
    async componentDidMount() {
        const [vehiclesResp, makesResp] = await Promise.all([fetch('api/vehicles'), fetch('api/makes')]);
        const [vehicles, makes] = await Promise.all([vehiclesResp.json(), makesResp.json()])

        this.setState({
            vehicles,
            makes
        })
    }

    handleMakeChange = (event) => {
        this.setState({
            selectedMakeId: parseInt(event.target.value)
        }, this.applyFilters);        
    }

    applyFilters = async () => {
        const queryParams = this.buildQueryParams();
        const vehiclesEndpoint = `api/vehicles?${queryParams}`;
        const vehiclesResp = await fetch(vehiclesEndpoint);
        const vehicles = await vehiclesResp.json();
        this.setState({
            vehicles
        });
    }

    buildQueryParams = () => {
        const {selectedMakeId} = this.state;
        const params = [];
        if (selectedMakeId !== DEFAULT_SELECETED_MAKE_ID) {
            params.push(`makeId=${selectedMakeId}`)
        }

        return params.join('&');
    }
}
 
export default VehiclesList;