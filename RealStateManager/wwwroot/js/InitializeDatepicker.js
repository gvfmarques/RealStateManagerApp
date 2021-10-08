$(document).ready(function () {
    $("#date").datepicker({
        autoclose: true,
        format: "dd/mm/yyyy",
        defaultDate: new Date(),
        setDefaultDate: true,
        disableWeekends: false,
        showDaysInNextandPreviousMonths: true,
        showMonthAfterYear: true,
        showClearBtn: true,

        i18n: {
            cancel: 'Cancel',
            clear: 'Clean',
            done: 'Ok',
            previousMonth: '‹',
            nextMonth: '›',
            months: [
                'January',
                'February',
                'March',
                'April',
                'May',
                'Juny',
                'July',
                'August',
                'September',
                'October',
                'November',
                'December'
            ],
            monthsShort: [
                'Jan',
                'Fev',
                'Mar',
                'Abr',
                'Mai',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dec'
            ],
            weekdays: [
                'Sunday',
                'Segunda-feira',
                'Tuesday',
                'Wednesday',
                'Thursday',
                'Friday',
                'Saturday'
            ],
            weekdaysShort: [
                'Sun',
                'Mon',
                'Tue',
                'Wed',
                'Thu',
                'Fri',
                'Sat'
            ],
        }
    });
})