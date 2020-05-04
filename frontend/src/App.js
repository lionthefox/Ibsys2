import React from 'react';
import Header from './components/Header';
import FileUpload from './components/FileUpload';

import * as FilePond from 'filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
import FilePondPluginFileEncode from 'filepond-plugin-file-encode';

import './components/animations.css';
import 'filepond/dist/filepond.min.css';

FilePond.registerPlugin(FilePondPluginFileValidateType);
FilePond.registerPlugin(FilePondPluginFileEncode);

const App = () => {
  return (
    <>
      <Header />
      <FileUpload multipleFiles={false} url='/simulation' />
    </>
  );
};

export default App;
