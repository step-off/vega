import React, { Component } from 'react';
import VehicleItem from './VehicleItem';

class VehiclesList extends Component {
    state = { 
        vehicles: []
     }
    render() { 
        return <ul>
            {this.state.vehicles.map(i => <VehicleItem vehicle={i}/>)}
        </ul>;
    }
    async componentDidMount() {
        const resp = await fetch('api/vehicles');
        const result = await resp.json();
        console.log(result);
        this.setState({
            vehicles: result
        })
    }
}
 
export default VehiclesList;