export const getValue = (val, decimal, maxValue, minValue) => {
  let newVal = val
    ? decimal
      ? parseFloat(val.replace('-', ''))
      : parseInt(val.replace('-', ''))
    : 0;
  if (newVal > maxValue) {
    newVal = maxValue;
  }
  if (minValue && newVal < minValue) {
    newVal = minValue;
  }
  return newVal;
};

export const getInputValue = (val) => String(val).replace('^0+', '');
