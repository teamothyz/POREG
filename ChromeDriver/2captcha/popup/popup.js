var background = chrome.runtime.connect({name: "popup"});

background.onMessage.addListener(function (msg) {

    if (msg.action == "login") {
        let btn = $("#login-form button[type=submit]");
        btn.text(btn.attr("data-text"));

        if (msg.error === undefined) {
            $("#login-form").hide();
            displayAccountInfo(msg.response);
        } else {
            $("#login-form").find(".result").html(`<div class="error">${msg.error}</div>`);
        }

        btn.removeClass('loading');
    }

    if (msg.action == "logout") {
        $("#account-info").hide();
        $("#login-form .result").html('&nbsp;');
        $("#login-form").show();
        $("#login-form").find("input").focus();
    }

    if (msg.action == "getAccountInfo") {
        $('.main-loader').hide();

        if (msg.error === undefined) {
            displayAccountInfo(msg.response);
        } else {
            $("#login-form").show().find("input").focus();
        }
    }
});

background.postMessage({action: "getAccountInfo"});

$("#login-form").submit(function (e) {
    e.preventDefault();

    let btn = $(this).find("button[type=submit]");
    btn.addClass('loading');

    $(this).find(".result").html("&nbsp;");

    let apiKey = $(this).find("input[name=apiKey]").val();

    background.postMessage({action: "login", apiKey});
});

$("#account-info .logout").click(function (e) {
    e.preventDefault();
    background.postMessage({action: "logout"});
});

$("#isPluginEnabled").change(function(e) {
   const isPluginEnabled = $(this).prop('checked');
   const on = $('.auto-solver-on');
   const off = $('.auto-solver-off');
   if (isPluginEnabled) {
       on.show();
       off.hide();
   } else {
       on.hide();
       off.show();
   }
});

function displayAccountInfo(info) {
    let block = $("#account-info");
    block.find('.email').text(info.email);
    block.find('.balance').text((info.valute == "USD" ? "$" : "₽") + " " + info.balance);
    block.show();
}

$("#settings-form").on("keyup change", "input,select", function() {
    let key = $(this).attr("name");
    let value = $(this).val();

    if ($(this).is("[type=checkbox]")) {
        value = $(this).is(":checked");
    }

    let dataType = $(this).attr("data-type");

    if (dataType == 'int') value = parseInt(value) || 0;
    if (dataType == 'float') value = parseFloat(value) || 0.0;

    let config = {};
    config[key] = value;

    Config.set(config);
});

Config.getAll().then(config => {
    $("#settings-form").find("input,select").each(function() {
        let field = $(this);
        let value = config[field.attr("name")];

        if (field.attr("type") == "checkbox") {
            field.prop("checked", value);
        } else {
            field.val(value);
        }
    });

    $("#isPluginEnabled").trigger('change');
});