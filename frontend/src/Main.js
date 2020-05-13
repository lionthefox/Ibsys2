import React, { Component } from 'react';
import { renderToStaticMarkup } from 'react-dom/server';
import { withLocalize, Translate } from 'react-localize-redux';
import { Route, Redirect } from 'react-router-dom';
import axios from 'axios';
import globalTranslations from './translations/global.json';

import Header from './components/navigation/Header';
import Input from './components/Upload/Input';
import Production from './components/Simulation/Production';
import QuantityPlanning from './components/Simulation/QuantityPlanning';
import CapacityPlanning from './components/Simulation/CapacityPlanning';
import SequencePlanning from './components/Simulation/SequencePlanning';
import OrderPlanning from './components/Simulation/OrderPlanning';
import Result from './components/Simulation/Result';
import Stepper from './components/navigation/Stepper';

import { setNestedObjectProperty } from './utils/nestedObjectProps';

const paths = [
  '/input',
  '/production',
  '/quantity_planning',
  '/capacity_planning',
  '/sequence_planning',
  '/order_planning',
  '/result',
];

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
    simulationInput: {
      forecast: {
        periode1: {
          produkt1: 0,
          produkt2: 0,
          produkt3: 0,
        },
        periode2: {
          produkt1: 0,
          produkt2: 0,
          produkt3: 0,
        },
        periode3: {
          produkt1: 0,
          produkt2: 0,
          produkt3: 0,
        },
        periode4: {
          produkt1: 0,
          produkt2: 0,
          produkt3: 0,
        },
      },
      vertriebswunsch: {
        produkt1: 0,
        produkt2: 0,
        produkt3: 0,
        direktverkauf: {
          produkt1: {
            menge: 0,
            preis: 0.0,
            konventionalstrafe: 0.0,
          },
          produkt2: {
            menge: 0,
            preis: 0.0,
            konventionalstrafe: 0.0,
          },
          produkt3: {
            menge: 0,
            preis: 0.0,
            konventionalstrafe: 0.0,
          },
        },
      },
    },
    simulationData: undefined,
    showError: false,
    errorMessageId: undefined,
    errorMessage: undefined,
  };

  changeActiveLanguage = (val) => {
    this.setState({ activeLanguage: val });
    this.props.setActiveLanguage(val);
  };

  setSimulationData = (val) => this.setState(val);

  setError = (error, errorMessageId, errorMessage) =>
    this.setState({
      showError: error,
      errorMessageId: errorMessageId,
      errorMessage: errorMessage,
    });

  handleNext = () => {
    const { history } = this.props;
    const { simulationInput, activeStep } = this.state;
    const { setSimulationData, setError } = this;

    let newState = { activeStep: activeStep + 1 };
    if (activeStep === 1) {
      axios({
        url: '/simulation/start',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        data: simulationInput,
      })
        .then(function (response) {
          if (response.status >= 200 && response.status < 300) {
            setError(false, undefined, undefined);
            newState.simulationData = response.data;
            setSimulationData(newState);
            history.push(paths[activeStep + 1]);
          } else {
            setError(true, 'Main.error.serverError', response);
            setTimeout(() => setError(false, undefined, undefined), 10000);
          }
        })
        .catch(function (errorMessage) {
          const response = errorMessage.response;
          let translateId = 'Main.error.serverError';
          if (response.status >= 500) {
            translateId = 'Main.error.uploadError';
          }
          setError(true, translateId, response);
          setTimeout(() => setError(false, undefined, undefined), 10000);
        });
    } else {
      this.setState(newState);
      history.push(paths[activeStep + 1]);
    }
  };

  handleBack = () => {
    const { history } = this.props;
    const { activeStep } = this.state;

    this.setState((prevState) => {
      return { activeStep: prevState.activeStep - 1 };
    });
    history.push(activeStep ? paths[activeStep - 1] : paths[0]);
  };

  handleReset = () => this.setState({ activeStep: 0 });

  setLastPeriodResults = (val) => this.setState({ lastPeriodResults: val });

  setSimulationInput = (keyArray, val) =>
    this.setState((prevState) => {
      const newSimulationInput = setNestedObjectProperty(
        prevState.simulationInput,
        keyArray,
        val
      );
      return { simulationInput: newSimulationInput };
    });

  render() {
    const {
      activeLanguage,
      activeStep,
      lastPeriodResults,
      simulationInput,
      simulationData,
      showError,
      errorMessageId,
      errorMessage,
    } = this.state;

    const inputProps = {
      language: activeLanguage,
      setLastPeriodResults: this.setLastPeriodResults,
      handleNext: this.handleNext,
      showError,
      errorMessageId,
      errorMessage,
      setError: this.setError,
    };

    const stepperProps = {
      paths,
      activeStep,
      language: activeLanguage,
      handleNext: this.handleNext,
      handleBack: this.handleBack,
      handleReset: this.handleReset,
      showError,
      errorMessageId,
      errorMessage,
    };

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
          render={() => (
            <AnimationWrapper>
              <Input {...inputProps} />
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/production'
          render={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={<Translate id='Headline.production' />}
              >
                <Production
                  simulationInput={simulationInput}
                  setSimulationInput={this.setSimulationInput}
                  lastPeriodResults={lastPeriodResults}
                />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/quantity_planning'
          render={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={
                  <Translate id='Headline.quantity_planning' />
                }
              >
                <QuantityPlanning simulationData={simulationData} />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        <Route
          exact
          path='/capacity_planning'
          render={() => (
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
          render={() => (
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
          render={() => (
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
          render={() => (
            <AnimationWrapper>
              <HeadlineWrapper
                headlineComponent={<Translate id='Headline.result' />}
              >
                <Result />
              </HeadlineWrapper>
            </AnimationWrapper>
          )}
        />
        {activeStep !== 0 ? <Stepper {...stepperProps} /> : null}
      </>
    );
  }
}

export default withLocalize(Main);
