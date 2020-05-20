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
