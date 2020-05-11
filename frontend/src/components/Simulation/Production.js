import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import { Translate } from 'react-localize-redux';

const styles = {
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
    marginBottom: '1rem',
  },
  value: {
    display: 'flex',
    height: '3rem',
    alignItems: 'center',
  },
};

const Production = ({ classes, lastPeriodResults }) => {
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

  return (
    <form noValidate autoComplete='off'>
      <div className={classes.root}>
        <Text
          label={<Translate id='Production.product' />}
          text={[
            <Translate id='Production.child_bike' />,
            <Translate id='Production.woman_bike' />,
            <Translate id='Production.man_bike' />,
          ]}
        />
        <Form label={lastPeriodResults ? lastPeriodResults.period + 1 : 0} />
        <Form label={lastPeriodResults ? lastPeriodResults.period + 2 : 0} />
        <Form label={lastPeriodResults ? lastPeriodResults.period + 3 : 0} />
        <Form label={lastPeriodResults ? lastPeriodResults.period + 4 : 0} />
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
    </form>
  );
};

export default withStyles(styles)(Production);
