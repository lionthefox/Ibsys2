export const getValue = (val, decimal, maxValue) => {
  let newVal = val
    ? decimal
      ? parseFloat(val.replace('-', ''))
      : parseInt(val.replace('-', ''))
    : 0;
  if (newVal > maxValue) {
    newVal = maxValue;
  }
  return newVal;
};

export const getInputValue = (val) => String(val).replace('^0+', '');
