"use strict";

function Maintenance() {

    var MH = MaintenanceHandler();
    var allDatabases;

    function allDatabasesArrived(data) {
        allDatabases = data;
        var $table = $("#DatabasesTable");

        var $tableBody = $("#DatabasesTableBody");
        $tableBody.empty();

        for (let i = 0; i < data.length; i++) {
            addDatabaseDtoInfoRow($table, data[i]);
        }
    }

    function inputFieldValueChanged() {
        var dto = $(this).data("dto");
        var propertyName = $(this).data("propertyName");
        var index = $(this).data("index");
        var newValue = $(this).val();

        if (typeof index !== "undefined") dto[propertyName][index] = newValue;
        else dto[propertyName] = newValue;
    }

    function createInputField(dto, propertyName, index) {
        var propertyValue = dto[propertyName];
        if (typeof index !== "undefined") propertyValue = propertyValue[index];

        var $input = $('<input type="text" value="' + propertyValue + '" />');
        $input.data("dto", dto);
        $input.data("propertyName", propertyName);
        if (typeof index !== "undefined") $input.data("index", index);
        $input.on('input', inputFieldValueChanged);
        return $input;
    }

    function addDatabaseDtoInfoRow($table, dto) {
        var $tr = $table.find('tbody:last').append('<tr>');
        var $nameInput = createInputField(dto, "name");

        var $td = $('<td> </td>').html($nameInput);
        $tr.append($td);
        $tr.append('<td>' + dto.hash + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');

        var $action = $('<input type="button" value="Save Database"/>');
        $action.click({ dto: dto }, saveDatabase);
        $td = $('<td> </td>').html($action);
        $tr.append($td);
        

        for (let i = 0; i < dto.racks.length; i++) {
            addRackInfoRow($table, dto.racks[i]);
        }
    }

    function addRackInfoRow($table, rackDto) {
        var $tr = $table.find('tbody:last').append('<tr>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        var $td = $('<td> </td>').html(createInputField(rackDto, "name"));
        $tr.append($td);
        $tr.append('<td>' + rackDto.hash + '</td>');

        for (let i = 0; i < rackDto.mountPoints.length; i++) {
            addMountPointRow($table, rackDto, i);
        }
    }

    function addMountPointRow($table, rackDto, index) {
        var $tr = $table.find('tbody:last').append('<tr>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        var $td = $('<td> </td>').html(createInputField(rackDto, "mountPoints", index));
        $tr.append($td);
    }

    //var activeDatabase;
    //function createNewDatabase() {
    //    var newName = $('#ActiveDatabaseNameInput').val();
    //    if (newName) {
    //        MH.createNewDatabase(newName)
    //            .then(allDatabasesArrived);
    //    }
    //}

    function saveDatabase(e) {
        var dto = e.data.dto;
        MH.saveDatabase(dto);
    }

    $(function () {
        MH.getAllDatabases()
            .then(allDatabasesArrived);
    });
}

Maintenance();
