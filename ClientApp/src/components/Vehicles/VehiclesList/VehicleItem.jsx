import React, { Component } from 'react';

class VehicleItem extends Component {
    render() { 
        const {vehicle} = this.props;

        return <li>
            <span>{`id: ${vehicle.id}`}</span>
        </li>;
    }
}
 
export default VehicleItem;