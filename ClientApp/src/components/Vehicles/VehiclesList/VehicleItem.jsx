import React, { Component } from 'react';

class VehicleItem extends Component {
    render() { 
        const {vehicle} = this.props;
        
        return  <tr>
            <td>{vehicle.id}</td>
            <td>{vehicle.name}</td>
        </tr>
    }
}
 
export default VehicleItem;