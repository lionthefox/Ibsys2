import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import { Translate } from 'react-localize-redux';

import ContainedTabs from '../ui_components/ContainedTabs';

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
  },
  columnContainer: {
    display: 'flex',
    flexDirection: 'column',
  },
  inputField: {
    marginBottom: '10px',
  },
  headerLabel: {
    fontWeight: 1000,
    marginBottom: '1rem',
  },
  value: {
    display: 'flex',
    height: '3rem',
    alignItems: 'center',
  },
};

const Production = ({ classes, lastPeriodResults }) => {
  const [index, setIndex] = useState(0);

  const Form = ({ label }) => (
    <div className={classes.columnContainer}>
      {label ? <div className={classes.headerLabel}>{label}</div> : null}
      <TextField
        className={classes.inputField}
        type='number'
        variant='outlined'
      />
      <TextField
        className={classes.inputField}
        type='number'
        variant='outlined'
      />
      <TextField
        className={classes.inputField}
        type='number'
        variant='outlined'
      />
    </div>
  );

  const Text = ({ label, prop, text }) => (
    <div className={classes.columnContainer}>
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
              label={lastPeriodResults ? lastPeriodResults.period + 1 : 0}
            />
            <Form
              label={lastPeriodResults ? lastPeriodResults.period + 2 : 0}
            />
            <Form
              label={lastPeriodResults ? lastPeriodResults.period + 3 : 0}
            />
            <Form
              label={lastPeriodResults ? lastPeriodResults.period + 4 : 0}
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
            <Form label={<Translate id='Production.amount' />} />
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
            <Form label={<Translate id='Production.amount' />} />
            <Form label={<Translate id='Production.price' />} />
            <Form label={<Translate id='Production.penalty' />} />
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
