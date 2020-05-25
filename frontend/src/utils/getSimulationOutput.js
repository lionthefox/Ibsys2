const getSimulationOutput = (
  lastPeriodResults,
  simulationInput,
  simulationData,
  capacityPlan,
  orderPlan
) => {
  const getOrderList = () => {
    const orderList = [];
    orderPlan.map((art) => {
      orderList.push({
        article: art.matNr,
        quantity: art.menge,
        modus: art.bestellart,
      });
    });
    return orderList;
  };

  const getProductionList = () => {
    const productionList = [];
    const products = ['p1', 'p2', 'p3'];
    products.map((p) =>
      simulationData[p].map((art) => {
        productionList.push({
          article: art.articleId,
          quantity: art.produktion,
        });
      })
    );
    return productionList;
  };

  const getWorkingTimeList = () => {
    const workingTimeList = [];
    capacityPlan.map((plc) => {
      workingTimeList.push({
        station: plc.arbeitsplatzId,
        shift: plc.anzSchicht,
        overtime: plc.ubermin,
      });
    });
    return workingTimeList;
  };

  const output = {
    user: {
      game: lastPeriodResults.game,
      group: lastPeriodResults.group,
      period: lastPeriodResults.period,
    },
    qualitycontrol: { type: 'yes', losequantity: 0, delay: 0 },
    sellwish: [
      {
        article: 1,
        quantity: simulationInput.vertriebswunsch.produkt1,
      },
      {
        article: 2,
        quantity: simulationInput.vertriebswunsch.produkt2,
      },
      {
        article: 3,
        quantity: simulationInput.vertriebswunsch.produkt3,
      },
    ],
    selldirect: [
      {
        article: 1,
        quantity: simulationInput.vertriebswunsch.direktverkauf.produkt1.menge,
        price: simulationInput.vertriebswunsch.direktverkauf.produkt1.preis,
        penalty:
          simulationInput.vertriebswunsch.direktverkauf.produkt1
            .konventionalstrafe,
      },
      {
        article: 2,
        quantity: simulationInput.vertriebswunsch.direktverkauf.produkt2.menge,
        price: simulationInput.vertriebswunsch.direktverkauf.produkt2.preis,
        penalty:
          simulationInput.vertriebswunsch.direktverkauf.produkt2
            .konventionalstrafe,
      },
      {
        article: 3,
        quantity: simulationInput.vertriebswunsch.direktverkauf.produkt3.menge,
        price: simulationInput.vertriebswunsch.direktverkauf.produkt3.preis,
        penalty:
          simulationInput.vertriebswunsch.direktverkauf.produkt3
            .konventionalstrafe,
      },
    ],
    orderlist: getOrderList(),
    productionlist: getProductionList(),
    workingtimelist: getWorkingTimeList(),
  };
  console.log(output);
  return output;
};

export default getSimulationOutput;
