import React from 'react';
import { LocalizeProvider } from 'react-localize-redux';
import Production from '../components/Simulation/Production';

export default {
  title: 'Production',
  component: Production,
};

export const ProductionExample = () => (
  <LocalizeProvider>
    <Production />
  </LocalizeProvider>
);
