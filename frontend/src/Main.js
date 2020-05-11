import React, { Component } from 'react';
import { renderToStaticMarkup } from 'react-dom/server';
import { withLocalize } from 'react-localize-redux';
import { Route, Redirect } from 'react-router-dom';
import globalTranslations from './translations/global.json';

import Header from './components/Header';
import Input from './components/Upload/Input';
import Production from './components/Simulation/Production';
import QuantityPlanning from './components/Simulation/QuantityPlanning';
import CapacityPlanning from './components/Simulation/CapacityPlanning';
import SequencePlanning from './components/Simulation/SequencePlanning';
import OrderPlanning from './components/Simulation/OrderPlanning';
import Result from './components/Simulation/Result';
import Stepper from './components/Stepper';

const AnimationWrapper = ({ children }) => (
  <div
    className='cssanimation sequence fadeInBottom'
    style={{ paddingTop: '13rem', paddingBottom: '15rem' }}
  >
    {children}
  </div>
);

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

  state = {
    activeLanguage: 'en',
    activeStep: 0,
    lastPeriodResults: undefined,
  };

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

  setLastPeriodResults = (val) => this.setState({ lastPeriodResults: val });

  render() {
    const { activeLanguage, activeStep, lastPeriodResults } = this.state;

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
          component={() => (
            <AnimationWrapper>
              <Input
                language={activeLanguage}
                setLastPeriodResults={this.setLastPeriodResults}
                handleNext={this.handleNext}
              />
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/production'
          component={() => (
            <AnimationWrapper>
              <Production lastPeriodResults={lastPeriodResults} />
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/quantity_planning'
          component={() => (
            <AnimationWrapper>
              <QuantityPlanning />
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/capacity_planning'
          component={() => (
            <AnimationWrapper>
              <CapacityPlanning />
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/sequence_planning'
          component={() => (
            <AnimationWrapper>
              <SequencePlanning />
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/order_planning'
          component={() => (
            <AnimationWrapper>
              <OrderPlanning />
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/result'
          component={() => (
            <AnimationWrapper>
              <Result />
            </AnimationWrapper>
          )}
        />
        {activeStep !== 0 ? (
          <Stepper
            activeStep={activeStep}
            language={activeLanguage}
            lastPeriodResults={lastPeriodResults}
            handleNext={this.handleNext}
            handleBack={this.handleBack}
            handleReset={this.handleReset}
          />
        ) : null}
      </>
    );
  }
}

export default withLocalize(Main);
