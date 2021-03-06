import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';

import ContainedTabs from '../ui_components/ContainedTabs';
import Form from '../ui_components/Form';
import Text from '../ui_components/Text';
import { Paper } from '@material-ui/core';

const styles = {
  wrapper: {
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  root: {
    display: 'flex',
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'space-around',
  },
  forecastContainer: {
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'flex-end',
    position: 'relative',
    top: '-5px',
  },
  forecastHeading: {
    width: '100%',
    display: 'flex',
    flexDirection: 'row',
  },
  period: {
    display: 'flex',
    height: '2rem',
    width: '80%',
    justifyContent: 'center',
    alignItems: 'center',
    color: '#135444',
    fontSize: '20px',
    position: 'relative',
    top: '1.3rem',
    zIndex: 1000,
  },
  product: {
    width: '22%',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    color: '#135444',
    fontSize: '20px',
    height: '2rem',
    position: 'relative',
    top: '1.3rem',
    zIndex: 1000,
  },
  paperSmall: {
    height: 'calc(100vh - 480px)',
    maxHeight: '300px',
    marginTop: '1rem',
    padding: '0rem 1rem 0.5rem',
    overflow: 'auto',
  },
  paper: {
    height: 'calc(100vh - 480px)',
    maxHeight: '310px',
    marginTop: '1rem',
    paddingBottom: '0.5rem',
    width: '960px',
    overflow: 'auto',
  },
};

const Production = ({
  classes,
  simulationInput,
  setSimulationInput,
  lastPeriodResults,
}) => {
  const [index, setIndex] = useState(0);

  const getComponents = () => {
    const formProps = {
      obj: simulationInput,
      setObjState: setSimulationInput,
    };

    switch (index) {
      case 0:
        return (
          <Paper classes={{ root: classes.paper }} elevation={3}>
            <div className={classes.forecastContainer}>
              <div className={classes.forecastHeading}>
                <div className={classes.product}>
                  <Translate id='Production.product' />
                </div>
                <div className={classes.period}>
                  <Translate id='Production.period' />
                </div>
              </div>
              <div className={classes.root}>
                <Text
                  margin
                  label={<div style={{ height: '1.3rem', width: '7rem' }} />}
                  text={[
                    <Translate id='Bike.child_bike' />,
                    <Translate id='Bike.woman_bike' />,
                    <Translate id='Bike.man_bike' />,
                  ]}
                />
                <Form
                  {...formProps}
                  label={lastPeriodResults ? lastPeriodResults.period + 1 : 0}
                  values={[
                    ['forecast', 'periode1', 'produkt1'],
                    ['forecast', 'periode1', 'produkt2'],
                    ['forecast', 'periode1', 'produkt3'],
                  ]}
                />
                <Form
                  {...formProps}
                  label={lastPeriodResults ? lastPeriodResults.period + 2 : 0}
                  values={[
                    ['forecast', 'periode2', 'produkt1'],
                    ['forecast', 'periode2', 'produkt2'],
                    ['forecast', 'periode2', 'produkt3'],
                  ]}
                />
                <Form
                  {...formProps}
                  label={lastPeriodResults ? lastPeriodResults.period + 3 : 0}
                  values={[
                    ['forecast', 'periode3', 'produkt1'],
                    ['forecast', 'periode3', 'produkt2'],
                    ['forecast', 'periode3', 'produkt3'],
                  ]}
                />
                <Form
                  {...formProps}
                  label={lastPeriodResults ? lastPeriodResults.period + 4 : 0}
                  values={[
                    ['forecast', 'periode4', 'produkt1'],
                    ['forecast', 'periode4', 'produkt2'],
                    ['forecast', 'periode4', 'produkt3'],
                  ]}
                />
              </div>
            </div>
          </Paper>
        );
      case 1:
        return (
          <Paper classes={{ root: classes.paperSmall }} elevation={3}>
            <div className={classes.root}>
              <Text
                margin
                label={<Translate id='Production.product' />}
                text={[
                  <Translate id='Bike.child_bike' />,
                  <Translate id='Bike.woman_bike' />,
                  <Translate id='Bike.man_bike' />,
                ]}
              />
              <Form
                {...formProps}
                label={<Translate id='Production.amount' />}
                values={[
                  ['vertriebswunsch', 'produkt1'],
                  ['vertriebswunsch', 'produkt2'],
                  ['vertriebswunsch', 'produkt3'],
                ]}
              />
            </div>
          </Paper>
        );
      case 2:
        return (
          <Paper classes={{ root: classes.paper }} elevation={3}>
            <div className={classes.root}>
              <Text
                margin
                label={<Translate id='Production.product' />}
                text={[
                  <Translate id='Bike.child_bike' />,
                  <Translate id='Bike.woman_bike' />,
                  <Translate id='Bike.man_bike' />,
                ]}
              />
              <Form
                {...formProps}
                label={<Translate id='Production.amount' />}
                values={[
                  ['vertriebswunsch', 'direktverkauf', 'produkt1', 'menge'],
                  ['vertriebswunsch', 'direktverkauf', 'produkt2', 'menge'],
                  ['vertriebswunsch', 'direktverkauf', 'produkt3', 'menge'],
                ]}
              />
              <Form
                {...formProps}
                decimal
                label={<Translate id='Production.price' />}
                values={[
                  ['vertriebswunsch', 'direktverkauf', 'produkt1', 'preis'],
                  ['vertriebswunsch', 'direktverkauf', 'produkt2', 'preis'],
                  ['vertriebswunsch', 'direktverkauf', 'produkt3', 'preis'],
                ]}
              />
              <Form
                {...formProps}
                decimal
                label={<Translate id='Production.penalty' />}
                values={[
                  [
                    'vertriebswunsch',
                    'direktverkauf',
                    'produkt1',
                    'konventionalstrafe',
                  ],
                  [
                    'vertriebswunsch',
                    'direktverkauf',
                    'produkt2',
                    'konventionalstrafe',
                  ],
                  [
                    'vertriebswunsch',
                    'direktverkauf',
                    'produkt3',
                    'konventionalstrafe',
                  ],
                ]}
              />
              <Text
                margin
                obj={lastPeriodResults.warehousestock.article}
                idProp='id'
                idArray={[1, 2, 3]}
                label={<Translate id='Production.stock' />}
                prop='amount'
              />
            </div>
          </Paper>
        );
      default:
        return <div>Production</div>;
    }
  };

  return (
    <div className={classes.wrapper}>
      <ContainedTabs
        tabs={[
          { label: <Translate id='Production.forecast' /> },
          { label: <Translate id='Production.sell_wish' /> },
          { label: <Translate id='Production.direct_sales' /> },
        ]}
        value={index}
        onChange={(e, i) => setIndex(i)}
      />
      <form noValidate autoComplete='off'>
        {getComponents()}
      </form>
    </div>
  );
};

export default withStyles(styles)(Production);
