import React, { useState } from 'react';
import Header from './components/Header';
import Simulation from './components/Simulation';
import Stepper from './components/Stepper';

import * as FilePond from 'filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
import FilePondPluginFileEncode from 'filepond-plugin-file-encode';

import './components/animations.css';
import 'filepond/dist/filepond.min.css';

FilePond.registerPlugin(FilePondPluginFileValidateType);
FilePond.registerPlugin(FilePondPluginFileEncode);

const App = () => {
  const [language, setLanguage] = useState('English');
  const changeLanguage = (val) => setLanguage(val);

  return (
    <>
      <Header language={language} setLanguage={changeLanguage} />
      <Simulation language={language} />
      <Stepper />
    </>
  );
};

export default App;
