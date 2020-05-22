import axios from 'axios';

export const postSimulationInput = (simulationInput, requestProps) => {
  const {
    newState,
    setNewState,
    setError,
    history,
    activeStep,
    paths,
  } = requestProps;
  axios({
    url: '/simulation/start',
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    data: simulationInput,
  })
    .then(function (response) {
      if (response.status >= 200 && response.status < 300) {
        if (response.status === 204) {
          setError(true, 'Main.error.noContent', response);
          setTimeout(() => setError(false, undefined, undefined), 10000);
        } else {
          setError(false, undefined, undefined);
          newState.simulationData = response.data;
          setNewState(newState);
          history.push(paths[activeStep + 1]);
        }
      } else {
        setError(true, 'Main.error.serverError', response);
        setTimeout(() => setError(false, undefined, undefined), 10000);
      }
    })
    .catch(function (errorMessage) {
      const response = errorMessage.response;
      let translateId = 'Main.error.serverError';
      if (response && response.status >= 500) {
        translateId = 'Main.error.uploadError';
      }
      setError(true, translateId, response);
      setTimeout(() => setError(false, undefined, undefined), 10000);
    });
};

export const putSimulationData = (
  product,
  newSimulationData,
  newState,
  setNewState,
  setError
) =>
  axios({
    url: `simulation/update-dispo-ef/${product}`,
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    data: newSimulationData[product],
  })
    .then(function (response) {
      if (response.status >= 200 && response.status < 300) {
        if (response.status === 204) {
          setError(true, 'Main.error.noContent', response);
          setTimeout(() => setError(false, undefined, undefined), 10000);
        } else {
          setError(false, undefined, undefined);
          newState.simulationData[product] = response.data;
          setNewState(newState);
        }
      } else {
        setError(true, 'Main.error.serverError', response);
        setTimeout(() => setError(false, undefined, undefined), 10000);
      }
    })
    .catch(function (errorMessage) {
      const response = errorMessage.response;
      let translateId = 'Main.error.serverError';
      if (response && response.status >= 500) {
        translateId = 'Main.error.uploadError';
      }
      setError(true, translateId, response);
      setTimeout(() => setError(false, undefined, undefined), 10000);
    });

export const getCapacityPlan = (requestProps) => {
  const {
    newState,
    setNewState,
    setError,
    history,
    activeStep,
    paths,
  } = requestProps;
  axios({
    url: `simulation/kapa-plan`,
    method: 'GET',
  })
    .then(function (response) {
      if (response.status >= 200 && response.status < 300) {
        if (response.status === 204) {
          setError(true, 'Main.error.noContent', response);
          setTimeout(() => setError(false, undefined, undefined), 10000);
        } else {
          setError(false, undefined, undefined);
          newState.capacityPlan = response.data;
          setNewState(newState);
          history.push(paths[activeStep + 1]);
        }
      } else {
        setError(true, 'Main.error.serverError', response);
        setTimeout(() => setError(false, undefined, undefined), 10000);
      }
    })
    .catch(function (errorMessage) {
      const response = errorMessage.response;
      let translateId = 'Main.error.serverError';
      if (response && response.status >= 500) {
        translateId = 'Main.error.uploadError';
      }
      setError(true, translateId, response);
      setTimeout(() => setError(false, undefined, undefined), 10000);
    });
};

export const getOrderPlan = (requestProps) => {
  const {
    newState,
    setNewState,
    setError,
    history,
    activeStep,
    paths,
  } = requestProps;
  axios({
    url: `simulation/kauf-dispo`,
    method: 'GET',
  })
    .then(function (response) {
      if (response.status >= 200 && response.status < 300) {
        if (response.status === 204) {
          setError(true, 'Main.error.noContent', response);
          setTimeout(() => setError(false, undefined, undefined), 10000);
        } else {
          setError(false, undefined, undefined);
          newState.orderPlan = response.data;
          setNewState(newState);
          history.push(paths[activeStep + 1]);
        }
      } else {
        setError(true, 'Main.error.serverError', response);
        setTimeout(() => setError(false, undefined, undefined), 10000);
      }
    })
    .catch(function (errorMessage) {
      const response = errorMessage.response;
      let translateId = 'Main.error.serverError';
      if (response && response.status >= 500) {
        translateId = 'Main.error.uploadError';
      }
      setError(true, translateId, response);
      setTimeout(() => setError(false, undefined, undefined), 10000);
    });
};

export const putOrderPlan = (newOrderPlan, newState, setNewState, setError) =>
  axios({
    url: `simulation/update-kauf-dispo`,
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    data: newOrderPlan,
  })
    .then(function (response) {
      if (response.status >= 200 && response.status < 300) {
        if (response.status === 204) {
          setError(true, 'Main.error.noContent', response);
          setTimeout(() => setError(false, undefined, undefined), 10000);
        } else {
          setError(false, undefined, undefined);
          console.log(response.data);
          newState.orderPlan = response.data;
          setNewState(newState);
        }
      } else {
        setError(true, 'Main.error.serverError', response);
        setTimeout(() => setError(false, undefined, undefined), 10000);
      }
    })
    .catch(function (errorMessage) {
      const response = errorMessage.response;
      let translateId = 'Main.error.serverError';
      if (response && response.status >= 500) {
        translateId = 'Main.error.uploadError';
      }
      setError(true, translateId, response);
      setTimeout(() => setError(false, undefined, undefined), 10000);
    });
