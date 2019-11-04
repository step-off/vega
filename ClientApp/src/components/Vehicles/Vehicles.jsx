import React, { Component } from 'react';
import {VehicleForm} from './VehicleForm/VehicleForm';
import {Switch, Route} from "react-router-dom";

class Vehicles extends Component {
    render() {
        const {path} = this.props.match;

        return <Switch>
            <Route exact path={path}>
                <h1>Vehicles</h1>
            </Route>
            <Route path={`${path}/new`} component={VehicleForm}/>
        </Switch>;
    }
}
 
export {Vehicles};