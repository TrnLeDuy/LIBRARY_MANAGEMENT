const searchBar = document.querySelector('.menu-input');

searchBar.addEventListener('click', () => {
    searchBar.classList.remove('menu-input--active');
    searchBar.classList.add('menu-input--focus');
})

searchBar.addEventListener('blur', () => {
    searchBar.classList.remove('menu-input--focus');
    searchBar.classList.add('menu-input--active');
})

const arrowButton = document.querySelector('.menu-icon');

arrowButton.addEventListener('click', () => {
    arrowButton.classList.remove('fa-chevron-left');
    arrowButton.classList.add('fa-chevron-right');
})

arrowButton.addEventListener('click', () => {
    arrowButton.classList.remove('fa-chevron-right');
    arrowButton.classList.add('fa-chevron-left');
})