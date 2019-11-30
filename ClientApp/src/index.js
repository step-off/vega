import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { createBrowserHistory } from "history";
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import {AuthProvider} from './providers/AuthProvider';
import config from "./auth_config.json";

const history = createBrowserHistory();
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const onRedirectCallback = appState => {
  history.push(
    appState && appState.targetUrl
      ? appState.targetUrl
      : window.location.pathname
  );
};

ReactDOM.render(
  <AuthProvider
    domain={config.domain}
    client_id={config.clientId}
    redirect_uri={window.location.origin}
    onRedirectCallback={onRedirectCallback}
  >
    <BrowserRouter basename={baseUrl}>
      <App />
    </BrowserRouter>
  </AuthProvider>,
rootElement);

registerServiceWorker();
