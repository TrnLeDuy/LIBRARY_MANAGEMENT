const searchBar = document.querySelector('.menu-input');

searchBar.addEventListener('click', () => {
    searchBar.classList.remove('menu-input--active');
    searchBar.classList.add('menu-input--focus');
})

searchBar.addEventListener('blur', () => {
    searchBar.classList.remove('menu-input--focus');
    searchBar.classList.add('menu-input--active');
})