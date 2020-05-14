import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';

import ContainedTabs from '../ui_components/ContainedTabs';
import Form from '../ui_components/Form';
import Text from '../ui_components/Text';

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

const QuantityPlanning = ({ classes, simulationData, setSimulationData }) => {
  const [index, setIndex] = useState(0);

  console.log(simulationData);

  const getComponent = () => {
    const formProps = {
      classes,
      simulationData,
      setSimulationData,
    };

    switch (index) {
      case 0:
        return (
          <div className={classes.root}>
            <div>Hi</div>
          </div>
        );
      case 1:
        return (
          <div className={classes.root}>
            <div>Second</div>
          </div>
        );
      case 2:
        return (
          <div className={classes.root}>
            <div>Third</div>
          </div>
        );
      default:
        return <div>QuantityPlanning</div>;
    }
  };

  return (
    <div className={classes.wrapper}>
      <ContainedTabs
        tabs={[
          { label: <Translate id='Bike.child_bike' /> },
          { label: <Translate id='Bike.woman_bike' /> },
          { label: <Translate id='Bike.man_bike' /> },
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

export default withStyles(styles)(QuantityPlanning);
