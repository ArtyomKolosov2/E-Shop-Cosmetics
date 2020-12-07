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

function delElement(arr, index)
{
    let newArr = [];
    for (let i in arr) {
        if (i == index) {
            continue;
        }
        newArr.push(arr[i]);
    }
    return newArr;
}

function addProduct(product) {
    const products = document.getElementById('products');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="food-row">
        <button class="btn-counter-close" id="btn_delete_food_row">&times;</button>
        <span class="food-name">${product.name}</span>
        <strong class="food-price">${product.cost} br</strong>
        <div class="food-counter">
            <button class="btn-counter" id="btn-counter">-</button>
            <span class="counter" min="0" max="10000">${product.number}</span>
            <button class="btn-counter" id="btn-counter">+</button>
        </div>
    </div>
    `;
    products.appendChild(block);
}

function createProducts(product) {
    for (let i in product) {
        addProduct(product[i]);
    }
}

function sumProducts(arrFoodPrice, foodPrice) {
    let sum = 0;
    for (let i = 0; i < foodPrice.length; i++) {
        sum += arrFoodPrice[i] * foodPrice[i].innerHTML;
    }
    return sum;
}

function parseFoodPrice(foodPrice) {
    let arrFoodPrice = []
    for (let i = 0; i < foodPrice.length; i++) {
        arrFoodPrice.push(Number(foodPrice[i].textContent.slice(0, -2)));
    }
    return arrFoodPrice;
}

function getPricetag() {
    const foodPrice = document.querySelectorAll('.food-price');
    const counter = document.querySelectorAll('.counter');
    let arrFoodPrice = parseFoodPrice(foodPrice);

    return sumProducts(arrFoodPrice, counter);
}

export function uploadCart() {
    createProducts(getCookie("products") ? JSON.parse(getCookie("products")) : []);

    document.querySelector('.modal-pricetag').innerHTML = getCookie("pricetag") ? getCookie("pricetag") : "0 br";
}

export function cartHandler() {
    $().ready(function () {
        $('.btn-counter-close').on('click', function () {
            let parent = $(this).parent();
            let index = Math.round($('.food-row').find('button').parent().index(parent) / 2);

            const foodRow = document.querySelectorAll(".food-row");
            foodRow[index].remove();

            let allProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
            allProducts = delElement(allProducts, index);

            const pricetag = getPricetag();
            const modalPricetag = document.querySelector('.modal-pricetag');

            modalPricetag.innerHTML = `${pricetag} br`;

            setCookie('products', JSON.stringify(allProducts));
            setCookie("pricetag", modalPricetag.innerHTML);
        });
        $('.btn-counter').on('click', function () {
            const newProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
            const counter = document.querySelectorAll('.counter');

            let parent = $(this).parent();
            let index = $('.food-counter').find('button').parent().index(parent);

            if (this.textContent === '+') {
                counter[index].innerHTML++;
            }
            else {
                counter[index].innerHTML--;
                if (counter[index].innerHTML < 1) {
                    counter[index].innerHTML = 1;
                }
            }
            const pricetag = getPricetag();
            const modalPricetag = document.querySelector('.modal-pricetag');

            modalPricetag.innerHTML = `${pricetag} br`;

            newProducts[index].number = Number(counter[index].innerHTML);

            setCookie("pricetag", modalPricetag.innerHTML);
            setCookie("products", JSON.stringify(newProducts));
        });
    });
}

function isContained(allProducts, product) {
    for (let i = 0; i < allProducts.length; i++) {
        if (allProducts[i]["id"] === product["id"]) {
            return true;
        }
    }
    return false;
}

export function addToCart()
{
    const productName = document.querySelector('.product-info__header');
    const cost = document.querySelector('.offer-footer__cost');
    let counter = document.querySelectorAll('.counter');

    const id = Number(document.location.href.slice(-1));
    const product = new Object();
    product["name"] = productName.innerHTML;
    product["cost"] = Number(cost.innerHTML.slice(0, -2));
    product["id"] = id;

    const allProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
    const index = allProducts.length - 1;
    // The product is contained in cart
    if (isContained(allProducts, product)) {
        counter[index].innerHTML = Number(counter[index].innerHTML) + 1;
        product["number"] = Number(counter[index].innerHTML);
        allProducts[index]["number"] = product["number"];
    } else {
        product["number"] = 1;
        addProduct(product);// add to basket
        allProducts.push(product); // push to array objects
    }

    let pricetag = getPricetag();
    document.querySelector('.modal-pricetag').innerHTML = `${pricetag} br`;

    setCookie("pricetag", `${pricetag} br`);
    setCookie('products', JSON.stringify(allProducts));

    cartHandler();
    console.log(allProducts);
}

function addCostToOrder(cost) {
    const products = document.getElementById('total-cost');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="total-cost">
        <span>Итого:</span><span>${cost}</span>
    </div>
    `;
    products.appendChild(block);
}

function addProductToOrder(product) {
    const products = document.getElementById('cart_info_body');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="cart-info__row">
        <span>Имя товара:</span><span>${product.name}</span>
    </div>
    <div class="cart-info__row">
        <span>Количество:</span><span>${product.number}</span>
    </div>
    <div class="cart-info__row">
        <span>Стоимость:</span><span>${product.cost} br</span>
    </div>
    <hr class="hr"/>
    `;
    products.appendChild(block);
}

function makingOrderProducts() {
    const products = getCookie("products") ? JSON.parse(getCookie("products")) : [];
    console.log(products);
    for (let i = 0; i < products.length; i++) {
        addProductToOrder(products[i]);
    }
}
function makingOrderTotalCost() {
    const pricetag = getCookie('pricetag') ? getCookie('pricetag') : '0 br';
    addCostToOrder(pricetag);
}

export async function makingOrder()
{
    await makingOrderProducts();
    await makingOrderTotalCost();
}