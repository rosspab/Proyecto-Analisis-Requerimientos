var idDivCuerpo = '#divCuerpo';

getJsGrid = function (control, configuration) {
    $.extend(jsGrid.Grid.prototype, {
        height: "425px",
        width: "100%",
        filtering: false,
        editing: false,
        inserting: false,
        sorting: true,
        paging: true,
        autoload: true,
        pageSize: 10,
        pageButtonCount: 10,
        confirmDeleting: false,
        pagerFormat: "Páginas: {first} {prev} {pages} {next} {last} {pageIndex} de {pageCount}",
        pagePrevText: "Anterior",
        pageNextText: "Próxima",
        pageFirstText: "Primera",
        pageLastText: "Última",
        loadMessage: "Por favor, espere...",
        noDataContent: "No hay registros",
        searchButton: false,
        clearFilterButton: false,
        searchButtonTooltip: "Buscar",
        invalidNotify: function (args) {
            $('#alert-error-not-submit').removeClass('hidden');
        },
        rowClick: function (args) {
            var $row = this.rowByItem(args.item);
            $('.jsgrid-cell-custom').removeClass('jsgrid-cell-custom');
            $row.children('.jsgrid-cell').addClass('jsgrid-cell-custom');
        }
    });

    var grid = $(control).jsGrid(configuration);

    return grid;
}

//Retorna false, si contiene código
//Retorna True, si el valor está libre de código.
function ValidarNumero(valor) {
    var expreg = /^[1-9][0-9]{0,2}$/;
    return expreg.test(valor);
}

//Retorna false, si contiene código
//Retorna True, si el valor está libre de código.
function ValidaInyeccionCodigo(valor) {
    var expreg = /<[^>]+>/;

    if (expreg.test(valor)) {
        return false;
    }
    else {
        return true;
    }
}

mostrarMensaje = function (tipoMensaje, mensaje) {
    if (!esVacio(mensaje)) {
        if (tipoMensaje === 'S') {
            alertify.success(mensaje);
        } else if (tipoMensaje === 'E') {
            alertify.error(mensaje);
        } else if (tipoMensaje === 'A') {
            alertify.warning(mensaje)
        }
    }
}

mostrarNotificacionUsuario = function (tipoMensaje, mensajeUsuario, mensajeTecnico) {
    var tipoAlerta = 'success';
    var icono = 'ok-sign';
    var verDetalle = '<a href="#" id="hreVerDetalle" class="alert-link" data-toggle="collapse" data-target="#divMensajeError">Ver detalle...</a>';
    var collapse = 'collapse';

    $(".floating_alert").remove();

    if (tipoMensaje == '' || tipoMensaje == null || tipoMensaje == undefined || tipoMensaje == '�') {
        tipoMensaje = 'C';
    }

    if (mensajeUsuario == '' || mensajeUsuario == null || mensajeUsuario == undefined) {
        return;
    }

    switch (tipoMensaje) {
        case 'A':
            tipoAlerta = 'warning';
            icono = 'exclamation-sign';

            break;
        case 'E':
            tipoAlerta = 'danger';
            icono = 'remove-sign';

            break;
        case 'I':
            tipoAlerta = 'info';
            icono = 'info-sign';

            break;
        default:
            tipoAlerta = 'success';
            icono = 'ok-sign';
    }

    if (mensajeTecnico == '' || mensajeTecnico == null || mensajeTecnico == undefined) {
        verDetalle = '';
        collapse = '';
    }
    else {
        if (mensajeTecnico.length > 0) {
            verDetalle = '<a href="#" id="hreVerDetalle" class="alert-link" data-toggle="collapse" data-target="#divMensajeError"> Ver detalle...</a>';
            collapse = 'collapse';
        }
        else {
            verDetalle = '';
            collapse = '';
        }

        mensajeTecnico = '<tr>' +
            '<td></td>' +
            '<td>' +
            '<div id="divMensajeError" class="cs-board-error ' + collapse + '">' +
            '<span class="az-board-error">' +
            reemplazarCaracteresEspeciales(mensajeTecnico) +
            '</span>' +
            '</div>' +
            '</td>' +
            '<td></td>' +
            '</tr>';
    }

    $('<div class="floating_alert alert alert-' + tipoAlerta + '" role="alert">' +
        '<table>' +
        '<tr>' +
        '<td>' +
        '<span class="glyphicon glyphicon-' + icono + ' az-board-icon"></span>' +
        '</td>' +

        '<td>' +
        '<span id="spnMensajeUsuario" class="az-board-message">' +
        reemplazarCaracteresEspeciales(mensajeUsuario) +
        verDetalle +
        '</span>' +
        '</td>' +

        '<td>' +
        '<button id="btnCerrarAlerta" type="button" class="close">×</button>' +
        '</td>' +
        '</tr>' +
        mensajeTecnico +
        '</table>' +
        '</div>').appendTo('body');

    if (tipoMensaje == 'C' || tipoMensaje == 'I') {
        setTimeout(function () {
            $(".alert").alert('close');
        }, 7000);
    }

    $('#btnCerrarAlerta').click(function () {
        $(".alert").alert('close');
    });
}

formatearMensajeValidacion = function (mensajeColeccion) {
    var mensajeAlerta = '';

    if (mensajeColeccion != undefined && mensajeColeccion.length > 0) {
        mensajeAlerta = 'Campo(s) requerido(s):<br/>';
        for (var i = 0; i < mensajeColeccion.length; i++) {
            mensajeAlerta += '- ' + mensajeColeccion[i] + '<br/>';
        }
    }

    return mensajeAlerta;
}


dialogoEliminarRegistro = function (id, mensajeEliminacion) {
    alertify.confirm('Confirmación', mensajeEliminacion,
        function () {
            eliminar(id);
        },
        function () {

        }).set({ transition: 'zoom', 'labels': { ok: 'Confirmar', cancel: 'Cancelar' } });

    $('.ajs-ok').addClass('btn btn-primary btn-lg');
    $('.ajs-cancel').addClass('btn btn-secondary btn-lg');
}

String.prototype.replaceAll = function (stringFind, stringReplace) {
    var ex = new RegExp(stringFind.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1"), "g");
    return this.replace(ex, stringReplace);
};

limpiarOpcionesCombo = function (idCombo) {

    var options = $('#' + idCombo);
    options.empty();
}

limpiarCamposInsertGrid = function () {
    $('.jsgrid-insert-row').find('input').val(null);
    $('.jsgrid-insert-row').find('input[type=checkbox]').prop('checked', false);
}

limpiarGrid = function (idGrid) {
    $("#" + idGrid).jsGrid('option', 'data', []);
}

mostrarOcultarDiv = function (idDiv, mostrar) {

    if (mostrar) {
        $('#' + idDiv).show();
    } else {
        $('#' + idDiv).hide();
    }
}

function comboBoxChanged(objChanged, objHijo, jOpcionRel) {
    var opcionPadre = objChanged.options[objChanged.selectedIndex].value;
    if (opcionPadre != "-1" && !jOpcionRel.hasOwnProperty(opcionPadre)) {
        for (var index = 0; index < objHijo.options.length; index++) {
            if (objHijo[index].value != "-1") {
                objHijo[index].setAttribute("disabled", "disabled");
                objHijo[index].selected = false;
            }
        }
        objHijo.selectedIndex = 0;
        return;
    }

    var opcionesHijas = jOpcionRel.hasOwnProperty(opcionPadre) ?
        jOpcionRel[opcionPadre] : [];

    for (var index = 0; index < objHijo.options.length; index++) {
        if (encontrado(opcionesHijas, objHijo.options[index].value)) {
            objHijo[index].removeAttribute("disabled");
        } else {
            objHijo[index].setAttribute("disabled", "disabled");
        }
        if (objHijo[index].value != "-1") {
            objHijo[index].selected = false;
        }
        objHijo.selectedIndex = 0;
    }
}
function encontrado(arr, obj) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == obj) {
            return true;
        }
    }
    return false;
}