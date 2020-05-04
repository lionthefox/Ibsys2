import React from 'react';
import Header from './components/Header';
import FileUpload from './components/FileUpload';

import * as FilePond from 'filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';

import './components/animations.css';
import 'filepond/dist/filepond.min.css';

FilePond.registerPlugin(FilePondPluginFileValidateType);

const App = () => {
  return (
    <>
      <Header />
      <FileUpload multipleFiles={false} url='/simulation' />
    </>
  );
};

export default App;
