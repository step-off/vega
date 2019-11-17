import React, { Component } from 'react';
import {VehicleForm} from './VehicleForm/VehicleForm';
import {Switch, Route} from "react-router-dom";
import VehiclesList from './VehiclesList/VehiclesList';

class Vehicles extends Component {
    render() {
        const {path} = this.props.match;

        return <Switch>
            <Route exact path={path}>
                <VehiclesList />
            </Route>
            <Route path={`${path}/new`} component={VehicleForm}/>
        </Switch>;
    }
}
 
export {Vehicles};