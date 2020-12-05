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

function delProduct(productArray, index) {
    //let blocksArray = getCookie("products") ? JSON.parse(getCookie('products')) : [];
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

    //setCookie("products", JSON.stringify(blocksArray));
}

function addProduct(product) {
    const products = document.getElementById('products');
    const block = document.createElement('div');
    block.innerHTML = `
    <div class="food-row">
        <span class="food-name">${product.name}</span>
        <strong class="food-price">${product.cost}</strong>
        <div class="food-counter">
            <button class="btn-counter">-</button>
            <span class="counter" min="0" max="10000">${product.number}</span>
            <button class="btn-counter">+</button>
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

function getPricetag(foodPrice, counter) {
    let arrFoodPrice = parseFoodPrice(foodPrice);

    return sumProducts(arrFoodPrice, counter);
}

export function uploadCart() {
    createProducts(getCookie("products") ? JSON.parse(getCookie("products")) : []);

    document.querySelector('.modal-pricetag').innerHTML = getCookie("pricetag") ? getCookie("pricetag") : "0 br";
}

export function cartHandler() {
    $().ready(function () {
        $('.btn-counter').on('click', function () {
            const newProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
            const foodPrice = document.querySelectorAll('.food-price');
            const counter = document.querySelectorAll('.counter');

            let parent = $(this).parent();
            let index = $('.food-counter').find('button').parent().index(parent);

            if (this.textContent === '+') {
                counter[index].innerHTML++;
            }
            else {
                counter[index].innerHTML--;
                if (counter[index].innerHTML < 0) {
                    counter[index].innerHTML = 0;
                }
            }
            let pricetag = getPricetag(foodPrice, counter);
            let modalPricetag = document.querySelector('.modal-pricetag');
            setCookie("pricetag", modalPricetag.innerHTML);

            modalPricetag.innerHTML = `${pricetag} br`;
            newProducts[index].number = Number(counter[index].innerHTML);

            setCookie("products", JSON.stringify(newProducts));

            console.log(JSON.parse(getCookie("products")));
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

export function addToCart(btnAddProduct)
{
    const productName = document.querySelector('.product-info__header');
    const cost = document.querySelector('.offer-footer__cost');
    let counter = document.querySelectorAll('.counter');
    const id = Number(document.location.href.slice(-1));
    const product = new Object();
    product["name"] = productName.innerHTML;
    product["cost"] = cost.innerHTML;
    product["id"] = id;
    
    const index = id - 1;

    const allProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
    // The product is contained in cart
    if (isContained(allProducts, product)) {
        counter[index].innerHTML = Number(counter[index].innerHTML) + 1;
        product["number"] = Number(counter[index].innerHTML);
        allProducts[index]["number"] = product["number"];
    } else {
        product["number"] = 1;
        addProduct(product);// add to basket
        allProducts.push(product); // push to array objects
        counter = document.querySelectorAll('.counter');
    }
    const foodPrice = document.querySelectorAll('.food-price');

    let pricetag = getPricetag(foodPrice, counter);
    document.querySelector('.modal-pricetag').innerHTML = `${ pricetag } br`

    setCookie("pricetag", pricetag);
    setCookie('products', JSON.stringify(allProducts));

    cartHandler();
    console.log(allProducts);
}

export function removeFromCart(btnAddProduct) {
    const productName = document.querySelector('.product-info__header');
    const cost = document.querySelector('.offer-footer__cost');
    let counter = document.querySelectorAll('.counter');
    const id = Number(document.location.href.slice(-1));
    const product = new Object();
    product["name"] = productName.innerHTML;
    product["cost"] = cost.innerHTML;
    product["id"] = id;

    const index = id - 1;

    const allProducts = getCookie("products") ? JSON.parse(getCookie("products")) : [];
    // The product is contained in cart
    if (isContained(allProducts, product)) {
        delProduct(product);// del in basket
        allProducts.splice(index); // push to array objects
    }
    const foodPrice = document.querySelectorAll('.food-price');

    setCookie("pricetag", getPricetag(foodPrice, counter));
    setCookie('products', JSON.stringify(allProducts));

    document.querySelector('.modal-pricetag').innerHTML = getCookie("pricetag") ? getCookie("pricetag") : "0 br";

    cartHandler();
    console.log(allProducts);
}
export function makingOrder(btnMakingProduct)
{
    (getCookie("products") ? JSON.parse(getCookie("products")) : []);
}