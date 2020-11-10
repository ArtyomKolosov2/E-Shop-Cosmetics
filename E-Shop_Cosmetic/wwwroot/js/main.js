function eventModalCart() {
    modalCart.classList.toggle("on-click");
}

const cardBtn = document.querySelector("#btn-basket");
const modalCart = document.querySelector(".modal-cart");
const close = document.querySelector(".btn-close");
const cancel = document.querySelector(".btn-cancel");

//modal cart
cardBtn.addEventListener("click", eventModalCart);

close.addEventListener("click", eventModalCart);

cancel.addEventListener("click", eventModalCart);


// day 1

const buttonAuth = document.querySelector('.button-auth');
const modalAuth = document.querySelector('.modal-auth');
const closeAuth = document.querySelector('.close-auth');
const loginForm = document.querySelector('#logInForm');
const loginInput = document.querySelector('#login');
const username = document.querySelector('.user-name');
const buttonOut = document.querySelector('.button-out');


let login = localStorage.getItem('Delivery');

function toggleModalAuth()
{
  loginInput.style.borderColor = '';
  modalAuth.classList.toggle("is-open");
}

function autorized(){
  console.log('Authorized');

  userName.textContent = login;

  buttonAuth.style.display = "none";
  username.style.display = 'inline';
  buttonOut.style.display = 'block';
  buttonOut.addEventListener('click', logOut);

  function logOut(){
    login = null;
    localStorage.removeItem('Delivery');
    buttonAuth.style.display = '';
    username.style.display = '';
    buttonOut.style.display = '';
    buttonOut.removeEventListener('click', logOut);
    
    checkAuth();
  }
}

function notAutorized(){
    console.log('Not authorized!');

    function login(event) {
        event.preventDefault();
        if (loginInput.value) {
            login = loginInput.value;

            localStorage.setItem('Delivery', login);

            toogleModalAuth();
            buttonAuth.removeEventListener('click', toggleModalAuth);
            closeAuth.removeEventListener('click', toggleModalAuth);
            loginForm.removeEventListener('submit', login);
            loginForm.reset();

            checkAuth();
        }
        else {
            loginInput.style.borderColor = 'red';
        }
    }
  buttonAuth.addEventListener('click', toggleModalAuth);
  closeAuth.addEventListener('click', toggleModalAuth);
  loginForm.addEventListener('submit', login);
}

function checkAuth()
{
    if (login){
        autorized();
    }
    else{
        notAutorized();
    }
}

checkAuth();


// basket

//длинна анимации
new WOW().init();