import React from 'react';

import { BrowserRouter as Router, Route } from 'react-router-dom';
import { LocalizeProvider } from 'react-localize-redux';
import Main from './Main';

import * as FilePond from 'filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
import FilePondPluginFileEncode from 'filepond-plugin-file-encode';

import './components/animations.css';
import 'filepond/dist/filepond.min.css';

FilePond.registerPlugin(FilePondPluginFileValidateType);
FilePond.registerPlugin(FilePondPluginFileEncode);

const App = (props) => (
  <LocalizeProvider>
    <Router>
      <Route path='/' component={Main} />
    </Router>
  </LocalizeProvider>
);

export default App;
