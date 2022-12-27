var Config = {

    default: {
        isPluginEnabled: true,
        apiKey: "@@@KEY@@@",
        valute: "USD",
        email: null,
        autoSubmitForms: false,
        submitFormsDelay: 0,
        enabledForRecaptchaV2: true,
        enabledForInvisibleRecaptchaV2: true,
        enabledForRecaptchaV3: true,
        autoSolveRecaptchaV2: true,
        autoSolveInvisibleRecaptchaV2: true,
        autoSolveRecaptchaV3: true,
        recaptchaV3MinScore: 0.5,
        repeatOnErrorTimes: 0,
        repeatOnErrorDelay: 0,
        useProxy: false,
        proxytype: "HTTP",
        proxy: "",
        autoSubmitRules: [{
            url_pattern: "(2|ru)captcha.com/demo",
            code: "" +
                '{"type":"source","value":"document"}' + "\n" +
                '{"type":"method","value":"querySelector","args":["button[type=submit]"]}' + "\n" +
                '{"type":"method","value":"click"}',
        }],
    },

    get: async function (key) {
        let config = await this.getAll();
        return config[key];
    },

    getAll: function () {
        return new Promise(function (resolve, reject) {
            chrome.storage.local.get('config', function (result) {
                resolve(Config.joinObjects(Config.default, result.config));
            });
        });
    },

    set: function (newData) {
        return new Promise(function (resolve, reject) {
            Config.getAll()
                .then(data => {
                    chrome.storage.local.set({
                        config: Config.joinObjects(data, newData)
                    }, function (config) {
                        resolve(config);
                    });
                });
        });
    },

    joinObjects: function (obj1, obj2) {
        let res = {};
        for (let key in obj1) res[key] = obj1[key];
        for (let key in obj2) res[key] = obj2[key];
        return res;
    },
};
