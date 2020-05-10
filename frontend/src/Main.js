import React, { Component } from 'react';
import { renderToStaticMarkup } from 'react-dom/server';
import { withLocalize } from 'react-localize-redux';
import globalTranslations from './translations/global.json';

import Header from './components/Header';
import Simulation from './components/Simulation';
import Stepper from './components/Stepper';

class Main extends Component {
  constructor(props) {
    super(props);

    this.props.initialize({
      languages: [
        { name: 'English', code: 'en' },
        { name: 'German', code: 'de' },
      ],
      translation: globalTranslations,
      options: { renderToStaticMarkup },
    });
  }

  state = { activeLanguage: 'en' };

  changeActiveLanguage = (val) => {
    this.setState({ activeLanguage: val });
    this.props.setActiveLanguage(val);
  };

  render() {
    const { activeLanguage } = this.state;
    return (
      <>
        <Header
          language={activeLanguage}
          setLanguage={this.changeActiveLanguage}
        />
        <Simulation language={activeLanguage} />
        <Stepper language={activeLanguage} />
      </>
    );
  }
}

export default withLocalize(Main);
