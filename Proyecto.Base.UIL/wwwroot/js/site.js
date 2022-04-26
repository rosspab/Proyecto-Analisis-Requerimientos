var $affectedElements = $(".cs-change-font-size");

$(document).ready(function () {
    $('.iq-numeric-amount').digits();

    $('.iq-sidebar .iq-menu li a').click(function () {
        $('.iq-sidebar .iq-menu li').removeClass('active');

        $(this).parent().addClass('active');
    });

    $('.iq-sidebar .iq-menu li ul li a').click(function () {
        $(this).parent().parent().parent().addClass('active');
    });
  
});

$.fn.digits = function () {
    return this.each(function () {
        var neg = false;

        if ($(this).text() < 0) {
            neg = true;
            $(this).text() = Math.abs($(this).text());
        }

        $(this).text((neg ? '-' : '') + parseFloat($(this).text(), 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString());
    })
}

$(document).on({
    ajaxStart: function () {
        if (typeof mostrarMensajeEspera == 'function') {
            mostrarMensajeEspera(false);
        }
    },
    ajaxStop: function () {
        if (typeof mostrarMensajeEspera == 'function') {
            mostrarMensajeEspera(false);
        }
    }
});

function obtenerIdUnico() {
    var fecha = new Date();
    var mil = fecha.getTime();

    return mil.toString();
}

function ocultarCargando() {
    jQuery('#loading').delay().fadeOut("");
}

function mostrarCargando() {
    jQuery('#loading').show();
}

mostrarMensajeEspera = function (mostrar) {
    //if (mostrar) {
    //    jQuery("#load").show();
    //    jQuery("#loading").show();
    //}
    //else {
    //    jQuery("#load").fadeOut();
    //    jQuery("#loading").delay().fadeOut("");
    //}
}

function mostrarMensajeConfirmacion (mensaje) {
    return confirm(reemplazarCaracteresEspeciales(mensaje));
}

function mostrarMensajeUsuario(tipoMensaje, mensajeUsuario, mensajeTecnico) {
    var id = obtenerIdUnico();
    var tipoAlerta = 'success';
    var icono = 'ok-sign';
    var verDetalle = '<a href="#" class="alert-link" data-toggle="collapse" data-target="#divMensajeError' + id + '">Ver detalles...</a>';
    var collapse = 'collapse';
    var delay = 5000;

    if (tipoMensaje == '' || tipoMensaje == null || tipoMensaje == undefined) {
        tipoMensaje = 'C';
    }

    if (mensajeUsuario == '' || mensajeUsuario == null || mensajeUsuario == undefined) {
        return;
    }

    switch (tipoMensaje) {
        case 'A':
            tipoAlerta = 'warning';
            icono = 'ri-alert-line';
            delay = 0;

            break;
        case 'E':
            tipoAlerta = 'danger';
            icono = 'ri-spam-2-line';
            delay = 0;

            break;
        case 'I':
            tipoAlerta = 'info';
            icono = 'ri-information-line';

            break;
        case 'S':
            tipoAlerta = 'secondary';
            icono = 'ri-settings-5-line';

            break;
        default:
            tipoAlerta = 'success';
            icono = 'ri-checkbox-circle-line';
    }

    if (mensajeTecnico == '' || mensajeTecnico == null || mensajeTecnico == undefined) {
        verDetalle = '';
        collapse = '';
    }
    else {
        if (mensajeTecnico.length > 0) {
            verDetalle = '<a href="#" class="alert-link" data-toggle="collapse" data-target="#divMensajeError' + id + '"> Ver detalles</a>';
            collapse = 'collapse';
        }
        else {
            verDetalle = '';
            collapse = '';
        }
    }

    $.notify({
        // options
        icon: icono,
        title: reemplazarCaracteresEspeciales(mensajeUsuario),
        message: reemplazarCaracteresEspeciales(mensajeTecnico),
        url: '',
        target: '_blank'
    }, {
        // settings
        type: tipoAlerta,
        delay: delay,
        showProgressbar: false,
        allow_dismiss: true,
        placement: {
            from: "top",
            align: "right"
        },
        offset: {
            x: 30,
            y: 95
        },
        template: '<div data-notify="container" class="col-11 col-sm-3 alert bg-{0}" role="alert">' +
            '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
            '<i data-notify="icon"></i> ' +
            '<span data-notify="title">{1} ' + verDetalle + '</span> ' +

            '<div id="divMensajeError' + id + '" class="cs-board-error ' + collapse + '">' +
            '<span data-notify="message">{2} </span>' +
            '</div>' +

            '<div class="progress" data-notify="progressbar">' +
            '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
            '</div>' +
            '<a href="{3}" target="{4}" data-notify="url"></a>' +
            '</div>'
    });
}

mostrarFeedbackCampo = function (tipo, mensaje, idControl) {
    if ($('#' + idControl + '').length > 0) {
        $('#' + idControl).html('');

        if (tipo == '' || tipo == null || tipo == undefined) {
            tipo = 'I';
        }

        if (mensaje == '' || mensaje == null || mensaje == undefined) {
            return;
        }

        switch (tipo) {
            case 'A':
                tipoAlerta = 'warning';
                icono = 'ri-alert-line';
                delay = 0;

                break;
            case 'E':
                tipoAlerta = 'danger';
                icono = 'ri-spam-2-line';
                delay = 0;

                break;
            case 'I':
                tipoAlerta = 'info';
                icono = 'ri-information-line';

                break;
            case 'S':
                tipoAlerta = 'secondary';
                icono = 'ri-settings-5-line';

                break;
            default:
                tipoAlerta = 'success';
                icono = 'ri-checkbox-circle-line';
        }

        $('#' + idControl).addClass('iq-feedback bg-' + tipoAlerta);
        $('#' + idControl).html('<i class="' + icono + '"></i>  ' + mensaje);
    }
}

reemplazarTodos = function (textoOriginal, textoBuscado, textoNuevo) {
    textoOriginal = (!textoOriginal.hasOwnProperty('message')) ? textoOriginal : textoOriginal.message;
    let resultado = textoOriginal;

    if ((textoOriginal == '' || textoOriginal == null || textoOriginal == undefined) &&
        (textoBuscado == '' || textoBuscado == null || textoBuscado == undefined)) {
        resultado = '';
    }
    if (textoNuevo == null || textoNuevo == undefined) {
        textoNuevo = '';
    }
    resultado = textoOriginal.replace(new RegExp(textoBuscado, 'g'), textoNuevo);

    return resultado;
}

reemplazarCaracteresEspeciales = function (textoOriginal) {
    var resultado = textoOriginal;

    if (textoOriginal == null || textoOriginal == undefined) {
        resultado = '';
    }

    resultado = reemplazarTodos(resultado, '&#161;', '¡');
    resultado = reemplazarTodos(resultado, '&#191;', '¿');

    resultado = reemplazarTodos(resultado, '&#193;', 'Á');
    resultado = reemplazarTodos(resultado, '&#201;', 'É');
    resultado = reemplazarTodos(resultado, '&#205;', 'Í');
    resultado = reemplazarTodos(resultado, '&#211;', 'Ó');
    resultado = reemplazarTodos(resultado, '&#218;', 'Ú');
    resultado = reemplazarTodos(resultado, '&#220;', 'Ü');

    resultado = reemplazarTodos(resultado, '&#225;', 'á');
    resultado = reemplazarTodos(resultado, '&#233;', 'é');
    resultado = reemplazarTodos(resultado, '&#237;', 'í');
    resultado = reemplazarTodos(resultado, '&#243;', 'ó');
    resultado = reemplazarTodos(resultado, '&#250;', 'ú');
    resultado = reemplazarTodos(resultado, '&#252;', 'ü');

    resultado = reemplazarTodos(resultado, '&amp;#225;', 'á');
    resultado = reemplazarTodos(resultado, '&amp;#233;', 'é');
    resultado = reemplazarTodos(resultado, '&amp;#237;', 'í');
    resultado = reemplazarTodos(resultado, '&amp;#243;', 'ó');
    resultado = reemplazarTodos(resultado, '&amp;#250;', 'ú');
    resultado = reemplazarTodos(resultado, '&amp;#252;', 'ü');

    resultado = reemplazarTodos(resultado, 'Ã¡', 'á');
    resultado = reemplazarTodos(resultado, 'Ã', 'í');
    resultado = reemplazarTodos(resultado, 'í³', 'ó');
    resultado = reemplazarTodos(resultado, 'Â', '');

    resultado = reemplazarTodos(resultado, '&lt;', '<');
    resultado = reemplazarTodos(resultado, '&gt;', '>');


    return resultado;
}

function irAInicio() {
    mostrarMensajeEspera(true);

    location.reload();
}

function imprimirPagina() {
    window.print();
}
function pulsar(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    return (tecla != 13);
}

