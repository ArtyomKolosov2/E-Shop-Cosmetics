function setCookie(name, value, exp_y, exp_m, exp_d, path = null, domain = null, secure = null) {
    let cookieString = name + "=" + escape(value);

    if (exp_y) {
        let expires = new Date(exp_y, exp_m, exp_d);
        cookieString += "; expires=" + expires.toGMTString();
    }
    if (path) {
        cookieString += "; path=" + escape(path);
    }
    if (domain) {
        cookieString += "; domain=" + escape(domain);
    }
    if (secure) {
        cookieString += "; secure";
    }

    document.cookie = cookieString;
}

function deleteCookie(cookieName) {
    let cookieDate = new Date();  // Текущая дата и время
    cookieDate.setTime(cookieDate.getTime() - 1);
    document.cookie = cookieName += "=; expires=" + cookieDate.toGMTString();
}

function getCookie(cookieName) {
    let results = document.cookie.match('(^|;) ?' + cookieName + '=([^;]*)(;|$)');

    if (results) {
        return (unescape(results[2]));
    }
    else {
        return null;
    }
}

export function setBasketData(userName) {
    if (!getCookie(userName)) {
        if (userName) {
            const currentDate = new Date;
            const cookieYear = currentDate.getFullYear() + 1;
            const cookieMonth = currentDate.getMonth();
            const cookieDay = currentDate.getDate();
            const data = [0, 0, 0, 0, 0, 0];
            SetCookie("BasketData", userName, data, cookieYear, cookieMonth, cookieDay);
        }
    }

    conter = [0, 0, 0, 0, 0, 0];
    cost = 0;
}

export async function getCounterProducts(userName) {
    return await getCookie(userName);
}

export async function getCostProducts(userName) {
    return await getCookie(userName);
}

const countNumber = (Range) => {
    let sum = 0;
    for (let i = 1; i < Range; i *= 10) {
        sum++;
    }
    return sum;
}

export function buildSlider(minRange, maxRange, $slider) {
    let $inputFrom = $("#priceMin");
    let $inputTo = $("#priceMax");
    let instance;
    let from = 0;
    let to = 0;

    $slider.ionRangeSlider({
        skin: "flat",
        type: "double",
        grid: true,
        postfix: "br",
        min: minRange,
        max: maxRange,
        from: minRange,
        to: maxRange,
        onStart: updateInputs,
        onChange: updateInputs,
        onFinish: updateInputs
    });

    instance = $slider.data("ionRangeSlider");

    function updateInputs(data) {
        from = data.from;
        to = data.to;

        $inputFrom.prop("value", from);
        $inputTo.prop("value", to);
    }

    $inputFrom.on("input", function () {
        let value = $(this).prop("value");

        /* validate */

        // down range
        if (value < countNumber(minRange)) {
            value = minRange;
        }
        // up range
        else if (value > maxRange) {
            if (value.length === countNumber(maxRange)) {
                value = maxRange - 1;
            }
            else {
                value = String(maxRange).slice(0, countNumber(maxRange)) - 1;
            }
        }
        // if zero
        else if (value[0] == 0, value.length > 1) {
            value = value.slice(0, 3);
        }

        instance.update({
            from: value
        });

        $(this).prop("value", value);
    });

    $inputTo.on("input", function () {
        var value = $(this).prop("value");

        // validate
        if (value > maxRange) {
            if (value.length === countNumber(maxRange)) {
                value = maxRange;
            }
            else {
                value = value.slice(0, countNumber(maxRange));
            }
        }

        instance.update({
            to: value
        });

        $(this).prop("value", value);
    });
}