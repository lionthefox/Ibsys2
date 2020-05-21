import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import { Paper } from '@material-ui/core';

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
    width: '80vw',
    flexDirection: 'column',
    justifyContent: 'space-around',
  },
  paper: {
    marginTop: '1rem',
    height: 'calc(100vh - 460px)',
    overflow: 'auto',
    padding: '10px',
  },
  elementContainer: {
    display: 'flex',
    width: '100%',
    height: '3rem',
    border: '3px solid #135444',
  },
};

const SequencePlanning = ({ classes, simulationData }) => {
  const [index, setIndex] = useState(0);
  const products = ['p1', 'p2', 'p3'];

  console.log(simulationData);
  const getComponents = () => {
    const elements = [];

    if (simulationData) {
      simulationData[products[index]].map((val, keyIndex) => {
        elements.push(
          <div
            className={classes.elementContainer}
            key={`sequence_planning_${products[index]}_${keyIndex}`}
          >
            {val.articleId}
          </div>
        );
      });
    }
    return (
      <div className={classes.root}>{simulationData ? elements : null}</div>
    );
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
      <Paper classes={{ root: classes.paper }} elevation={3}>
        {getComponents()}
      </Paper>
    </div>
  );
};

export default withStyles(styles)(SequencePlanning);
