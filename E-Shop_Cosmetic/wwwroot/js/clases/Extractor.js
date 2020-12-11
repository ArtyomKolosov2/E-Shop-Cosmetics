export default class Extractor {
    constructor(getCookie, setCookie) {
        this.getCookie = getCookie;
        this.setCookie = setCookie;
    }
    getProducts() {
        return this.getCookie("products") ? JSON.parse(JSON.parse(this.getCookie("products"))) : [];
    }
    getPricetag() {
        return (this.getCookie('pricetag') ? this.getCookie('pricetag') : 0);
    }
    setProducts(allProducts) {
        this.setCookie('products', JSON.stringify(JSON.stringify(allProducts)));
    }
    setPricetag(pricetag) {
        this.setCookie("pricetag", pricetag);
    }
}
