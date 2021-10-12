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