import React, { Component } from 'react';
import { renderToStaticMarkup } from 'react-dom/server';
import { withLocalize } from 'react-localize-redux';
import { Route, Switch, Redirect } from 'react-router-dom';
import globalTranslations from './translations/global.json';

import Header from './components/Header';
import Input from './components/Upload/Input';
import Forecast from './components/Simulation/Forecast';
import QuantityPlanning from './components/Simulation/QuantityPlanning';
import CapacityPlanning from './components/Simulation/CapacityPlanning';
import SequencePlanning from './components/Simulation/SequencePlanning';
import OrderPlanning from './components/Simulation/OrderPlanning';
import Result from './components/Simulation/Result';
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

  state = { activeLanguage: 'en', activeStep: 0 };

  changeActiveLanguage = (val) => {
    this.setState({ activeLanguage: val });
    this.props.setActiveLanguage(val);
  };

  handleNext = () =>
    this.setState((prevState) => {
      return { activeStep: prevState.activeStep + 1 };
    });
  handleBack = () =>
    this.setState((prevState) => {
      return { activeStep: prevState.activeStep - 1 };
    });
  handleReset = () => this.setState({ activeStep: 0 });

  render() {
    const { activeLanguage, activeStep } = this.state;
    return (
      <>
        <Header
          language={activeLanguage}
          setLanguage={this.changeActiveLanguage}
          handleReset={this.handleReset}
        />
        <Redirect from='/' to='/input' />
        <Route
          exact
          path='/input'
          component={() => <Input language={activeLanguage} />}
        />
        <Route exact path='/forecast' component={() => <Forecast />} />
        <Route
          exact
          path='/quantity_planning'
          component={() => <QuantityPlanning />}
        />
        <Route
          exact
          path='/capacity_planning'
          component={() => <CapacityPlanning />}
        />
        <Route
          exact
          path='/sequence_planning'
          component={() => <SequencePlanning />}
        />
        <Route
          exact
          path='/order_planning'
          component={() => <OrderPlanning />}
        />
        <Route exact path='result' component={() => <Result />} />
        <Stepper
          activeStep={activeStep}
          language={activeLanguage}
          handleNext={this.handleNext}
          handleBack={this.handleBack}
          handleReset={this.handleReset}
        />
      </>
    );
  }
}

export default withLocalize(Main);
