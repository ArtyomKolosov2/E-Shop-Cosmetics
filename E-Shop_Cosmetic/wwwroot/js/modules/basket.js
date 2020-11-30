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
    document.cookie = `${cookieName}=; expires=${cookieDate.toGMTString()}`;
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
function createProducts(product) {
    for (let i in product) {
        addProduct(product[i]);
    }
}
function addProduct(product) {
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
    products.appendChild(block);
}

function delProduct(productArray, index)
{
    //let blocksArray = getCookie("products") ? JSON.parse(getCookie('products')) : [];
    const products = document.getElementById('products');
    const block = `
    <div class="food-row">
        <span class="food-name">${ productArray[index].name }</span>
        <strong class="food-price">${ productArray[index].cost }</strong>
        <div class="food-counter">
            <button class="btn-counter">-</button>
            <span class="counter" min="0" max="10000">${ productArray[index].number }</span>
            <button class="btn-counter">+</button>
        </div>
    </div>
    `;
    blocksArray.removeChild(block.outerHTML);
    products.removeChild(block);

    //setCookie("products", JSON.stringify(blocksArray));
}

export function basketLogic() {
    const modalPricetag = document.querySelector('.modal-pricetag');
    let basketObj = [
        {
            name: "Ролл угорь стандарт",
            number: 3,
            cost: "6 br",
            id: 0
        },
        {
            name: "Да угорь",
            number: 1,
            cost: "5 br",
            id: 1
        },
        {
            name: "Хай гэйс",
            number: 3,
            cost: "2 br",
            id: 2
        },
        // priceTag = "0 br"
    ];
    createProducts(getCookie("products") ? JSON.parse(getCookie("products")) : basketObj);

    let newProducts = JSON.parse(getCookie("products"));

    modalPricetag.innerHTML = getCookie("priceTag");

    $().ready(function () {
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

            modalPricetag.innerHTML = `${priceTag} br`;
            newProducts[index].number = counter[index].innerHTML;

            setCookie("priceTag", modalPricetag.innerHTML);
            setCookie("products", JSON.stringify(newProducts));

            console.log(JSON.parse(getCookie("products")));
        });
    });
}