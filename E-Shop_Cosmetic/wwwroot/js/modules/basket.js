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

export function setBasketData(userName, data) {
    if (!getCookie(userName)) {
        if (userName) {
            const currentDate = new Date;
            const cookieYear = currentDate.getFullYear() + 1;
            const cookieMonth = currentDate.getMonth();
            const cookieDay = currentDate.getDate();
            SetCookie("BasketData", userName, data, cookieYear, cookieMonth, cookieDay);
        }
    }
}
const data = [0, 0, 0, 0, 0, 0];
conter = [0, 0, 0, 0, 0, 0];
cost = 0;

export async function getCounterProducts(userName) {
    return await getCookie(userName);
}

export async function getCostProducts(userName) {
    return await getCookie(userName);
}

function Sum(arr) {
    let sum = 0;
    for (let el of arr) {
        sum += el;
    }
    return sum;
}
export function basketLogic() {
    $().ready(function () {
        $('.btn-counter').click(function (e) {
            const FoodCounter = document.querySelectorAll('.food-counter');
            const FoodPrice = document.querySelectorAll('.food-price');
            let arrFoodPrice = [];
            for (let i = 0; i < FoodPrice.length; i++) {
                arrFoodPrice.push( Number(FoodPrice[i].textContent.slice(0, -2)) );
            }
            let index = FoodPrice(e.target);
            if (this.textContent === '+') {
                FoodCounter[index].innerHTML++;
            }
            else {
                FoodCounter[index].innerHTML--;
            }
            FoodPrice[index].innerHTML = arrFoodPrice[index] * FoodCounter[index].innerHTML;
            
            let priceTag = Sum(arrFoodPrice) | null;

            document.querySelector('.modal-pricetag').innerHTML = `${priceTag} ₽`;
        });
    });
}