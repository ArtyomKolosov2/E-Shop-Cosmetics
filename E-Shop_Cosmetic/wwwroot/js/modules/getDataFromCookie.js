function setCookie(name, value, exp_y, exp_m, exp_d, path, domain, secure) {
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

export async function getCounterProducts(userName)
{
    return await getCookie(userName);
}

export async function getCostProducts(userName)
{
    return await getCookie(userName);
}