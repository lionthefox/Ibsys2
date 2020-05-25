import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import clsx from 'clsx';

import artNumbers from '../../assets/artNumbers';

const styles = {
  textContainer: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  sticky: {
    position: 'sticky',
    left: 0,
    background: '#fff',
    zIndex: 1000,
  },
  margin: {
    marginRight: '1.5rem',
    marginLeft: '1.5rem',
  },
  headerLabel: {
    textAlign: 'center',
    fontSize: '20px',
    color: '#135444',
    background: '#fff',
    position: 'sticky',
    top: '0rem',
    zIndex: 100,
    padding: '1.5rem 1rem',
    width: '100%',
    textOverflow: 'ellipsis',
    whiteSpace: 'nowrap',
  },
  value: {
    display: 'flex',
    height: '25%',
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: '6px',
    textAlign: 'center',
  },
};

const Text = ({
  classes,
  obj,
  idProp,
  label,
  prop,
  text,
  idArray,
  productIDs,
  margin,
  stickyIDs,
}) => {
  const elements = [];
  const getArticleProp = (article) => {
    if (article[prop]) {
      if (prop === idProp && productIDs) {
        return artNumbers[article[prop] - 1];
      } else return article[prop];
    } else return 0;
  };

  if (prop) {
    if (idArray) {
      idArray.map((id) =>
        elements.push(
          <div key={`Text_${label || 'label'}_${id}`} className={classes.value}>
            {obj
              ? obj.map((art) => {
                  if (art[idProp] === id) {
                    return getArticleProp(art);
                  }
                })
              : 0}
          </div>
        )
      );
    } else {
      if (obj) {
        obj.map((art) =>
          elements.push(
            <div
              key={`Text_${label || 'label'}_${art[idProp]}`}
              className={classes.value}
            >
              {getArticleProp(art)}
            </div>
          )
        );
      }
    }
  } else if (text) {
    text.map((val, index) =>
      elements.push(
        <div
          key={`Production_text_${label || 'label'}_${index}`}
          className={classes.value}
        >
          {val}
        </div>
      )
    );
  }
  return (
    <div
      className={clsx(classes.textContainer, {
        [classes.margin]: margin,
        [classes.sticky]: stickyIDs,
      })}
    >
      {label ? <div className={classes.headerLabel}>{label}</div> : null}
      <>{elements}</>
    </div>
  );
};

export default withStyles(styles)(Text);
