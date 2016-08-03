$(function () {
    $('#fechaconf #FechaConfeccion').datetimepicker({
        format: 'DD/MM/YYYY HH:mm A',
        defaultDate: new Date(),
        daysOfWeekDisabled: [0, 6]
    });

    $('#fechavig #FechaVigencia').datetimepicker({
        format: 'DD/MM/YYYY HH:mm A',
        defaultDate: new Date(),
        daysOfWeekDisabled: [0, 6]
    });

    $('#fechadealta').datetimepicker({
        format: 'DD/MM/YYYY',
        defaultDate: new Date(),
        daysOfWeekDisabled: [0, 6]
    });
});