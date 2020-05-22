import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import { Paper, ListItemIcon, IconButton } from '@material-ui/core';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { DragDropContext, Droppable, Draggable } from 'react-beautiful-dnd';

import VerticalSplitRoundedIcon from '@material-ui/icons/VerticalSplitRounded';
import ContainedTabs from '../ui_components/ContainedTabs';
import Popover from '../ui_components/Popover';
import artNumbers from '../../assets/artNumbers';

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
    padding: '0 10px',
  },
  elementContainer: {
    display: 'flex',
    width: '100%',
    height: '3rem',
    marginBottom: '8px',
    background: '#fff',
    fontSize: '20px',
    borderRadius: '10px',
    boxShadow: '1px 2px 8px 0px rgba(194,194,194,0.5)',
    '&:hover': {
      background: 'rgba(19, 83, 67, 0.25)',
    },
    '&:active': {
      background: 'rgba(19, 83, 67, 0.8)',
    },
  },
  articleId: {
    width: '11.5%',
    paddingLeft: '10px',
  },
  name: {
    width: '66%',
  },
  productionAmount: {
    width: '15%',
  },
  splitButton: {
    position: 'relative',
    top: '3px',
    '&:hover': {
      backgroundColor: 'transparent',
    },
    '&:active': {
      backgroundColor: 'transparent',
    },
  },
  splitButtonIcon: {
    color: '#fff',
    padding: '5px',
    borderRadius: '10px',
    background: '#135444',
  },
  headerContainer: {
    display: 'flex',
    width: '100%',
    height: '4rem',
    background: '#fff',
    fontSize: '18px',
    color: '#135444',
    zIndex: 1000,
    position: 'sticky',
    top: 0,
    padding: '0.3rem 10px 0',
    margin: '0 -10px',
  },
  articleIdHeader: {
    width: '12.8%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
    paddingLeft: '1rem',
  },
  nameHeader: {
    width: '65%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
  },
  productionAmountHeader: {
    width: '10%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
    paddingRight: '65px',
  },
  splittingHeader: {
    height: '100%',
    display: 'flex',
    alignItems: 'center',
  },
  instructions: {
    marginTop: '10px',
    fontSize: '18px',
    color: '#135444',
  },
  splitPlaceholder: {
    width: '4.5%',
  },
};
//TODO: Merging
const SequencePlanning = ({
  classes,
  language,
  simulationData,
  setSimulationData,
}) => {
  const [index, setIndex] = useState(0);
  const [anchorEl, setAnchorEl] = useState(null);
  const [currentVal, setCurrentVal] = useState(null);
  const [currentIndex, setCurrentIndex] = useState(null);
  const [splittingValue1, setSplittingValue1] = useState(0);
  const [splittingValue2, setSplittingValue2] = useState(0);

  const products = ['p1', 'p2', 'p3'];

  const openPopover = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const closePopover = () => {
    setAnchorEl(null);
  };

  const open = Boolean(anchorEl);

  const setInitialSplittingValues = (val) => {
    const newSplittingValue1 =
      Math.floor(val.produktion / 2) + (val.produktion % 2);
    const newSplittingValue2 = Math.floor(val.produktion / 2);
    setSplittingValue1(newSplittingValue1);
    setSplittingValue2(newSplittingValue2);
  };

  const submitSplittingValues = () => {
    const newSimulationData = { ...simulationData };
    const element1 = { ...newSimulationData[products[index]][currentIndex] };
    const element2 = { ...newSimulationData[products[index]][currentIndex] };
    element1.produktion = splittingValue1;
    element2.produktion = splittingValue2;
    newSimulationData[products[index]].splice(currentIndex, 1);
    newSimulationData[products[index]].splice(currentIndex, 0, element2);
    newSimulationData[products[index]].splice(currentIndex, 0, element1);
    setSimulationData(newSimulationData);
  };

  const onDragEnd = (result) => {
    const { destination, source } = result;
    if (!destination) {
      return;
    }
    if (
      destination.droppableId === source.droppableId &&
      destination.index === source.index
    ) {
      return;
    }
    const newSimulationData = { ...simulationData };
    const draggedElement = newSimulationData[products[index]][source.index];
    newSimulationData[products[index]].splice(source.index, 1);
    newSimulationData[products[index]].splice(
      destination.index,
      0,
      draggedElement
    );
    setSimulationData(newSimulationData);
  };

  const getComponents = () => {
    const elements = [];

    if (simulationData) {
      simulationData[products[index]].map((val, keyIndex) => {
        const popoverProps = {
          val: currentVal,
          anchorEl,
          open,
          closePopover,
          splittingValue1,
          splittingValue2,
          setSplittingValue1,
          setSplittingValue2,
          submitSplittingValues,
        };
        elements.push(
          <Draggable
            draggableId={`sequence_planning_${products[index]}_${keyIndex}`}
            index={keyIndex}
            key={`sequence_planning_${products[index]}_${keyIndex}`}
          >
            {(provided) => (
              <ListItem
                {...provided.draggableProps}
                {...provided.dragHandleProps}
                innerRef={provided.innerRef}
                classes={{ root: classes.elementContainer }}
              >
                <ListItemText
                  classes={{ root: classes.articleId }}
                  primary={artNumbers[val.articleId - 1]}
                />
                <ListItemText
                  classes={{ root: classes.name }}
                  primary={language === 'en' ? val.nameEng : val.name}
                />
                <ListItemText
                  classes={{ root: classes.productionAmount }}
                  primary={val.produktion}
                />
                {val.produktion >= 20 ? (
                  <ListItemIcon>
                    <IconButton
                      className={classes.splitButton}
                      disableRipple
                      onClick={(e) => {
                        setInitialSplittingValues(val);
                        setCurrentVal(val);
                        setCurrentIndex(keyIndex);
                        openPopover(e);
                      }}
                    >
                      <VerticalSplitRoundedIcon
                        classes={{ root: classes.splitButtonIcon }}
                      />
                    </IconButton>
                    <Popover {...popoverProps} />
                  </ListItemIcon>
                ) : (
                  <div className={classes.splitPlaceholder}></div>
                )}
              </ListItem>
            )}
          </Draggable>
        );
      });
    }
    return (
      <div className={classes.root}>
        <div className={classes.headerContainer}>
          <div className={classes.articleIdHeader}>
            <Translate id='QuantityPlanning.articleId' />
          </div>
          <div className={classes.nameHeader}>
            <Translate id='QuantityPlanning.name' />
          </div>
          <div className={classes.productionAmountHeader}>
            <Translate id='QuantityPlanning.produktion' />
          </div>
          <div className={classes.splittingHeader}>
            <Translate id='SequencePlanning.splitting' />
          </div>
        </div>
        <Droppable droppableId='sequene_planning_droppable'>
          {(provided) => (
            <List
              component='nav'
              aria-label='sequence_planning_list'
              innerRef={provided.innerRef}
              {...provided.droppableProps}
            >
              {simulationData ? elements : null}
              {provided.placeholder}
            </List>
          )}
        </Droppable>
      </div>
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
      <div className={classes.instructions}>
        <Translate id='SequencePlanning.instructions' />
      </div>
      <Paper classes={{ root: classes.paper }} elevation={3}>
        <DragDropContext onDragEnd={onDragEnd}>
          {getComponents()}
        </DragDropContext>
      </Paper>
    </div>
  );
};

export default withStyles(styles)(SequencePlanning);
