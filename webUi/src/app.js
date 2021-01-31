import {PLATFORM} from 'aurelia-pal';

export class App {
  // this function is expected by aurelia when you have a router-view element in your view file.
  configureRouter(config, router) {
    // the text to display as the documents title (browser tab). It is combined with router.title and any child routes
    config.title = 'Spending Reports';

    // 'configures push state' https://aurelia.io/docs/routing/configuration#options
    // push state allows the url to change without reloading the page.
    // before push state was supported, hash-based routing was required to change the url without a reload
    // without push state: https://localhost:8000/#/fragment
    // with push state:    https://localhost:8000/fragment
    config.options.pushState = true;
    config.options.root = '/';
    
    // adds routes to the router
    config.map([
      { 
        // the pattern to match against incoming url fragments. Can be a string or array or strings. It can contain parameterized routes and wildcards.
        route: '', 
        // the id of the module that exports the component to be rendered when the route is matched
        moduleId: PLATFORM.moduleName('transactions/transaction-list'), 
        // the document title, combined with router.title and child routes
        title: 'Transactions', 
        // a friendly name to reference the route with 
        name: 'transactions-route'
      },
      { 
        route: 'categories',
        moduleId: PLATFORM.moduleName('categories/category-list'), 
        title: 'Categories', 
        name: 'transactions-route'
      }
    ]);
  }

  title = 'Spending Reports!';
}
