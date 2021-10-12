function AssembleGraphicLinesEarnings(year) {

    $.ajax({
        url: '/Dashboard/GraphicDataGains',
        method: 'GET',
        data: { year: year },
        success: function (data) {

            new Chart(document.getElementById("GraphValuesGainsExpenses"), {
                type: 'line',

                data: {
                    labels: GetMonths(data),
                    datasets: [{
                        label: "Earnings From Payments Per Year",
                        data: GetValues(data),
                        backgroundColor: "#90caf9",
                        borderColor: "#0277bd",
                        fill: true,
                        lineTension: 0
                    }]
                },

                options: {
                    legend: {
                        labels: {
                            usePointStyle: true
                        }
                    },

                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: false
                            }
                        }]
                    }
                }
            });
        }
    });
}

function AssembleGraphicLinesExpenses(year) {
    $.ajax({
        url: '/Dashboard/GraphicDataExpenses',
        method: 'GET',
        data: { year: year },
        success: function (data) {
            debugger;
            new Chart(document.getElementById("GraphValuesGainsExpenses"), {
                type: 'line',

                data: {
                    labels: GetMonths(data),
                    datasets: [{
                        label: "Service Expenses Per Year",
                        data: GetValues(data),
                        backgroundColor: "#ffcdd2",
                        borderColor: "#b71c1c",
                        fill: true,
                        lineTension: 0
                    }]
                },

                options: {
                    legend: {
                        labels: {
                            usePointStyle: true
                        }
                    },

                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: false
                            }
                        }]
                    }
                }
            });
        }
    });
}

function AssembleChartExpensesTotalGains() {
    $.ajax({
        url: '/Dashboard/GraphicDataExpensesTotalGains',
        method: 'GET',
        success: function (data) {
            new Chart(document.getElementById("GraphicDataExpensesTotalGains"), {

                type: 'pie',

                data: {
                    labels: ["Incomes", "Expenses"],

                    datasets: [{
                        label: "Total Earnings and Expenses",
                        data: [data.incomes, data.expenses],
                        backgroundColor: ["#0091ea", "#c62828"]
                    }]
                },

                options: {
                    legend: {
                        labels: {
                            usePointStyle: true
                        }
                    }
                }
            });
        }
    });
}
function GetMonths(data) {
    var labels = [];
    var size = data.length;
    var index = 0;

    while (index < size) {
        labels.push(data[index].months);
        index = index + 1;
    }

    return labels;
}

function GetValues(data) {
    var values = [];
    var size = data.length;
    var index = 0;

    while (index < size) {
        values.push(data[index].values);
        index = index + 1;
    }

    return values;
}