import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import { Translate } from 'react-localize-redux';

import ContainedTabs from '../ui_components/ContainedTabs';
import { getNestedObjectProperty } from '../../utils/nestedObjectProps';
import { getFloatValue } from '../../utils/getValue';

const Input = withStyles({
  root: {
    marginBottom: '10px',
    width: '10rem',

    '& label.Mui-focused': {
      color: '#135444',
    },
    '& .MuiInput-underline:after': {
      borderBottomColor: '#135444',
    },
    '& .MuiOutlinedInput-root': {
      '&.Mui-focused fieldset': {
        borderColor: '#135444',
      },
    },
  },
})(TextField);

const styles = {
  wrapper: {
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  root: {
    display: 'flex',
    justifyContent: 'space-around',
    marginTop: '2rem',
  },
  columnContainer: {
    display: 'flex',
    flexDirection: 'column',
    marginRight: '1rem',
  },
  textContainer: {
    display: 'flex',
    flexDirection: 'column',
    marginRight: '2rem',
    marginLeft: '2rem',
  },
  headerLabel: {
    marginBottom: '1.5rem',
    textAlign: 'center',
    fontSize: '18px',
  },
  value: {
    display: 'flex',
    height: '25%',
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: '3px',
  },
};

const Form = ({
  classes,
  simulationInput,
  setSimulationInput,
  label,
  values,
}) => {
  const inputProps = {
    type: 'number',
    variant: 'outlined',
  };

  const getInputValue = (val) => String(val).replace('^0+', '');

  return (
    <div className={classes.columnContainer}>
      {label ? <div className={classes.headerLabel}>{label}</div> : null}
      <Input
        {...inputProps}
        value={
          getInputValue(getNestedObjectProperty(simulationInput, values[0])) ||
          0
        }
        onChange={(e) => {
          setSimulationInput(values[0], getFloatValue(e.target.value));
        }}
      />
      <Input
        {...inputProps}
        value={
          getInputValue(getNestedObjectProperty(simulationInput, values[1])) ||
          0
        }
        onChange={(e) =>
          setSimulationInput(values[1], getFloatValue(e.target.value))
        }
      />
      <Input
        {...inputProps}
        value={
          getInputValue(getNestedObjectProperty(simulationInput, values[2])) ||
          0
        }
        onChange={(e) =>
          setSimulationInput(values[2], getFloatValue(e.target.value))
        }
      />
    </div>
  );
};

const Production = ({
  classes,
  simulationInput,
  setSimulationInput,
  lastPeriodResults,
}) => {
  const [index, setIndex] = useState(0);

  const Text = ({ label, prop, text }) => (
    <div className={classes.textContainer}>
      {label ? <div className={classes.headerLabel}>{label}</div> : null}
      {prop ? (
        <>
          <div className={classes.value}>
            {lastPeriodResults
              ? lastPeriodResults.warehousestock.article.map((art) => {
                  if (art.id === 1) return art[prop] || 0;
                })
              : 0}
          </div>
          <div className={classes.value}>
            {lastPeriodResults
              ? lastPeriodResults.warehousestock.article.map((art) => {
                  if (art.id === 2) return art[prop] || 0;
                })
              : 0}
          </div>
          <div className={classes.value}>
            {lastPeriodResults
              ? lastPeriodResults.warehousestock.article.map((art) => {
                  if (art.id === 3) return art[prop] || 0;
                })
              : 0}
          </div>
        </>
      ) : text ? (
        <>
          <div className={classes.value}>{text[0]}</div>
          <div className={classes.value}>{text[1]}</div>
          <div className={classes.value}>{text[2]}</div>
        </>
      ) : null}
    </div>
  );

  const getComponent = () => {
    const formProps = {
      classes,
      simulationInput,
      setSimulationInput,
    };

    switch (index) {
      case 0:
        return (
          <div className={classes.root}>
            <Text
              label={<Translate id='Production.product' />}
              text={[
                <Translate id='Production.child_bike' />,
                <Translate id='Production.woman_bike' />,
                <Translate id='Production.man_bike' />,
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
        );
      case 1:
        return (
          <div className={classes.root}>
            <Text
              label={<Translate id='Production.product' />}
              text={[
                <Translate id='Production.child_bike' />,
                <Translate id='Production.woman_bike' />,
                <Translate id='Production.man_bike' />,
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
        );
      case 2:
        return (
          <div className={classes.root}>
            <Text
              label={<Translate id='Production.product' />}
              text={[
                <Translate id='Production.child_bike' />,
                <Translate id='Production.woman_bike' />,
                <Translate id='Production.man_bike' />,
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
              label={<Translate id='Production.price' />}
              values={[
                ['vertriebswunsch', 'direktverkauf', 'produkt1', 'preis'],
                ['vertriebswunsch', 'direktverkauf', 'produkt2', 'preis'],
                ['vertriebswunsch', 'direktverkauf', 'produkt3', 'preis'],
              ]}
            />
            <Form
              {...formProps}
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
            <Text label={<Translate id='Production.stock' />} prop='amount' />
          </div>
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
        {getComponent()}
      </form>
    </div>
  );
};

export default withStyles(styles)(Production);
