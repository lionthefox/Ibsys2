import React, { Component } from 'react';
import { renderToStaticMarkup } from 'react-dom/server';
import { withLocalize, Translate } from 'react-localize-redux';
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
    style={{ paddingTop: '10rem', paddingBottom: '15rem' }}
  >
    {children}
  </div>
);

const HeadlineWrapper = ({ headlineComponent, children }) => (
  <div
    style={{
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
      width: '100%',
    }}
  >
    <div
      style={{
        textAlign: 'center',
        fontSize: '2rem',
        fontWeight: 1000,
        marginBottom: '1rem',
      }}
    >
      {headlineComponent || ''}
    </div>
    <div
      id='horizontal_line'
      style={{
        width: '90%',
        borderBottom: '3px solid #135444',
        marginBottom: '2rem',
      }}
    ></div>
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
              <HeadlineWrapper
                headlineComponent={<Translate id='Headline.production' />}
              >
                <Production lastPeriodResults={lastPeriodResults} />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/quantity_planning'
          component={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={
                  <Translate id='Headline.quantity_planning' />
                }
              >
                <QuantityPlanning />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/capacity_planning'
          component={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={
                  <Translate id='Headline.capacity_planning' />
                }
              >
                <CapacityPlanning />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/sequence_planning'
          component={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={
                  <Translate id='Headline.sequence_planning' />
                }
              >
                <SequencePlanning />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/order_planning'
          component={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={<Translate id='Headline.order_planning' />}
              >
                <OrderPlanning />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/result'
          component={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={<Translate id='Headline.result' />}
              >
                <Result />
              </HeadlineWrapper>
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
