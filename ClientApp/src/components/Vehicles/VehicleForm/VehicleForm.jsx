import React, { Component } from 'react';
import {AuthContext} from '../../../providers/AuthProvider'

class VehicleForm extends Component {
    state = { 
        makes: [],
        currentMakeId: '',
        currentModelId: '',
        features: [],
        chosenFeatures: [],
        isRegistered: true,
        contact: {
            name: '',
            phone: ''
        },
     }

    render() {       
        const {makes, currentMakeId, currentModelId, chosenFeatures} = this.state;
        const activeMake = makes.find(i => i.id.toString() === currentMakeId);
        const models = activeMake ? activeMake.models : [];

        return ( 
            <AuthContext.Consumer>
                {() => {
                    return (
                        <form onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label htmlFor="make">Make</label>
                                <select className="form-control" id="make" onChange={this.handleMakeChange}>
                                    <option value="" selected={!currentMakeId}>Not selected</option>
                                    {this.state.makes.map(make => (
                                        <option value={make.id} selected={currentMakeId === make.id} key={make.id}>{make.name}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="form-group">
                                <label htmlFor="model">Model</label>
                                <select className="form-control" id="model" onChange={this.handleModelChange}>
                                    <option value="" selected={!currentModelId}>Not selected</option>
                                    {models.map(j => (
                                        <option value={j.id} key={j.id} selected={currentModelId === j.id}>{j.name}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="form-group">
                            {this.state.features.map(i => {
                                return <label htmlFor={i.id} key={i.id}>
                                    <span>{i.name}</span>
                                    <input type="checkbox" id={i.id} value={i.id} onChange={this.handleFeatureChange} checked={chosenFeatures.includes(i.id)}/>
                                </label>
                            })} 
                            </div>
                            <div className="input-group">
                                Registered?
                                <div className="input-group-text">
                                    <label>
                                        Yes
                                        <input type="radio" name="isRegistered" value={true} checked={this.state.isRegistered} onChange={this.handleIsRegisteredChange}/>
                                    </label>
                                    <label>
                                        No
                                        <input type="radio" name="isRegistered" value={false} checked={!this.state.isRegistered} onChange={this.handleIsRegisteredChange}/>
                                    </label>
                                </div>
                            </div>
                            <div className="form-group">
                            <input type="text" className="form-control" placeholder="Name" value={this.state.contact.name} onChange={this.handleContactNameChange}/> 
                            <input type="text" className="form-control" placeholder="Phone" value={this.state.contact.phone} onChange={this.handleContactPhoneChange}/> 
                            </div>
                            <button type='submit' className="btn btn-primary">Send</button>
                        </form>
                    )
                }}
            </AuthContext.Consumer>
        );
    }

    componentDidMount() {
        this.fetchData()
    }

    fetchData = async () => {
        const [makesResp, featuresResp] = await Promise.all([
            fetch('api/makes'), 
            fetch('api/features')
        ]);
        let makes; 
        let features;
        try {
            makes = await makesResp.json(); 
            features = await featuresResp.json();
        } catch (e) {    
            return;
        }
        
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

    handleModelChange = (event) => {
        this.setState({
            currentModelId: event.target.value
        })
    }

    handleIsRegisteredChange = (event) => {       
        this.setState({
            isRegistered: event.target.value === "true"
        })
    }

    handleFeatureChange = (event) => {
        let {checked, value} = event.target;
        value = parseInt(value);
        const {chosenFeatures} = this.state;
        if (checked && !chosenFeatures.includes(value)) {
            this.setState({
                chosenFeatures: [...chosenFeatures, value]
            })
        }
        if (!checked && chosenFeatures.includes(value)) {
            const newChosenFeatures = [...chosenFeatures];
            newChosenFeatures.splice(chosenFeatures.indexOf(value), 1);
            this.setState({
                chosenFeatures: newChosenFeatures
            })
        }
    }

    handleContactNameChange = (event) => {
        this.setState({
            contact: {
                ...this.state.contact,
                name: event.target.value
            }
        })
    }
    
    handleContactPhoneChange = (event) => {
        this.setState({
            contact: {
                ...this.state.contact,
                phone: event.target.value
            }
        })
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        const vehiclePayload = this.getVehiclePayload();
        
        await fetch('api/vehicles', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(vehiclePayload)
        })
    }

    getVehiclePayload = () => {
        const {currentMakeId, currentModelId, isRegistered, chosenFeatures, contact} = this.state;
        return {
            make: [parseInt(currentMakeId)],
            vehicleModelId: parseInt(currentModelId),
            isRegistered: isRegistered,
            contact,
            features: chosenFeatures.map(i => parseInt(i))
        }
    }
}
 
export {VehicleForm};