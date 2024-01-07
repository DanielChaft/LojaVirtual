class Menu extends HTMLElement {
    connectedCallback() {
        this.innerHTML = `
            <nav class="topnav">
                <a href="home.html">Home</a>
                <a href="Cliente.html">Clientes</a>
                <a href="Produto.html">Produtos</a>
                <a href="Pedido.html">Pedidos</a>
                    <a href="javascript:void(0);" class="icon">
                        <span class="line"></span>
                        <span class="line"></span>
                        <span class="line"></span>
                    </a>
            </nav>
        `;

        const activePage = window.location.pathname;
        const navLinks = document.querySelectorAll('nav a').
        forEach(link => {
            if(link.href.includes(`${activePage}`)){
                link.classList.add('active');
            }
        })

    }
}

customElements.define('main-menu', Menu);