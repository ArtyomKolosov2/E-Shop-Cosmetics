function setCookie(name, value, exp_y, exp_m, exp_d, path = "/", domain = "", secure = true) {
    let cookieString = `${name}=${escape(value)}`;

    if (exp_y) {
        const expires = new Date(exp_y, exp_m, exp_d);
        cookieString += `; expires=${expires.toGMTString()}`;
    }
    if (path) {
        cookieString += `; path=${escape(path)}`;
    }
    if (domain) {
        cookieString += `; domain=${escape(domain)}`;
    }
    if (secure) {
        cookieString += "; secure";
    }

    document.cookie = cookieString;
}

function deleteCookie(cookieName) {
    let cookieDate = new Date();  // Текущая дата и время
    cookieDate.setTime(cookieDate.getTime() - 1);
    document.cookie = String(`${cookieName}=; expires=${cookieDate.toGMTString()}`);
}
function getCookie(cookieName) {
    let results = document.cookie.match(`(^|;) ?${cookieName}=([^;]*)(;|$)`);

    return results ? unescape(results[2]) : null;
}

function sumProducts(arrFoodPrice, foodPrice) {
    let sum = 0;
    for (let i = 0; i < foodPrice.length; i++) {
        sum += arrFoodPrice[i] * foodPrice[i].innerHTML;
    }
    return sum;
}

function parseFoodPrice(arrFoodPrice, foodPrice) {
    for (let i = 0; i < foodPrice.length; i++) {
        arrFoodPrice.push(Number(foodPrice[i].textContent.slice(0, -2)));
    }
}
// буду передовать куки продукта
function addProduct(product) {
    let blocksArray = getCookie('products') ? JSON.parse(getCookie('products')) : [];
    const products = document.getElementById('products');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="food-row">
        <span class="food-name">${ product.name }</span>
        <strong class="food-price">${ product.cost }</strong>
        <div class="food-counter">
            <button class="btn-counter">-</button>
            <span class="counter" min="0" max="10000">${ product.number }</span>
            <button class="btn-counter">+</button>
        </div>
    </div>
    `;
    blocksArray.push(block.outerHTML);
    products.appendChild(block);

    setCookie("products", JSON.stringify(blocksArray));
}

function delProduct(productArray, index) {
    let blocksArray = getCookie("products") ? JSON.parse(localStorage.getItem('products')) : [];
    const products = document.getElementById('products');
    const block = `
    <div class="food-row">
        <span class="food-name">${productArray[index].name}</span>
        <strong class="food-price">${productArray[index].cost}</strong>
        <div class="food-counter">
            <button class="btn-counter">-</button>
            <span class="counter" min="0" max="10000">${productArray[index].number}</span>
            <button class="btn-counter">+</button>
        </div>
    </div>
    `;
    blocksArray.removeChild(block.outerHTML);
    products.removeChild(block);

    setCookie("products", JSON.stringify(blocksArray));
}

function setProducts(productArray) {
    for (let i in productArray) {
        addProduct(productArray[i]);
    }
}

export function basketLogic() {
    $().ready(function () {
        let rowObj = [
            {
                name: "Ролл угорь стандарт",
                number: 3,
                cost: "6 br",
                index: 0
            },
            {
                name: "Да угорь",
                number: 1,
                cost: "5 br",
                index: 1
            },
            {
                name: "Хай гэйс",
                number: 3,
                cost: "2 br",
                index: 2
            },
        ];
        setProducts(rowObj);

        $('.btn-counter').on('click', function () {
            const foodPrice = document.querySelectorAll('.food-price');
            const counter = document.querySelectorAll('.counter');
            let arrFoodPrice = [];

            let parent = $(this).parent();
            let index = $('.food-counter').find('button').parent().index(parent);

            parseFoodPrice(arrFoodPrice, foodPrice);

            if (this.textContent === '+') {
                counter[index].innerHTML++;
            }
            else {
                counter[index].innerHTML--;
                if (counter[index].innerHTML < 0) {
                    counter[index].innerHTML = 0;
                }
            }

            let priceTag = sumProducts(arrFoodPrice, counter) | null;

            document.querySelector('.modal-pricetag').innerHTML = `${priceTag} br`;
        });
    });
}