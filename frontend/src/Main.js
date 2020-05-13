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
  };

  changeActiveLanguage = (val) => {
    this.setState({ activeLanguage: val });
    this.props.setActiveLanguage(val);
  };

  setSimulationData = (val) => this.setState(val);

  handleNext = () => {
    const { simulationInput, activeStep } = this.state;
    const { setSimulationData } = this;

    let newState = { activeStep: activeStep + 1 };
    if (activeStep === 1) {
      axios({
        url: '/simulation/start',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        data: simulationInput,
      })
        .then(function (response) {
          console.log(response);
          if (response.status >= 200 && response.status < 300) {
            newState.simulationData = response.data;
            console.log(newState);
            setSimulationData(newState);
          } else {
            alert(response.status, 'Servererror');
          }
        })
        .catch(function (errorMessage) {
          console.log(errorMessage.response);
          alert('Upload fehlgeschlagen');
        });
    } else {
      console.log(newState);
      this.setState(newState);
    }
  };

  handleBack = () =>
    this.setState((prevState) => {
      return { activeStep: prevState.activeStep - 1 };
    });
  handleReset = () => this.setState({ activeStep: 0 });

  setLastPeriodResults = (val) => this.setState({ lastPeriodResults: val });

  setSimulationInput = (keyArray, val) =>
    this.setState((prevState) => {
      const newSimulationInput = setNestedObjectProperty(
        prevState.simulationInput,
        keyArray,
        val
      );
      console.log(this.state.simulationInput);
      return { simulationInput: newSimulationInput };
    });

  render() {
    const {
      activeLanguage,
      activeStep,
      lastPeriodResults,
      simulationInput,
      simulationData,
    } = this.state;

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
        {activeStep !== 0 ? (
          <Stepper
            activeStep={activeStep}
            language={activeLanguage}
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
