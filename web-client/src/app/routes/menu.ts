const Home = {
    text: 'Dashboard',
    link: '/home',
    icon: 'icon-home'
};

const Material = {
    text: 'Material',
    link: '/material',
    icon: 'fa fa-shield-alt',
    submenu: [
        {
            text: 'Widgets',
            link: '/material/widgets'
        },
        {
            text: 'Cards',
            link: '/material/cards'
        },
        {
            text: 'Forms',
            link: '/material/forms'
        },
        {
            text: 'Inputs',
            link: '/material/inputs'
        },
        {
            text: 'Lists',
            link: '/material/lists'
        },
        {
            text: 'Whiteframe',
            link: '/material/whiteframe'
        },
        {
            text: 'Colors',
            link: '/material/colors'
        },
        {
            text: 'ng2Material',
            link: '/material/ngmaterial'
        }
    ],
    'alert': 'new',
    'label': 'badge badge-primary'
};

const Tenants = {
    text: 'Tenants',
    link: '/tenants/list',
    icon: 'icon-user'
};

const Properties = {
    text: 'Properties',
    link: '/properties/list',
    icon: 'icon-home'
};

const AssignedProperties = {
    text: 'Assigned Properties',
    link: '/assignedproperties/list',
    icon: 'icon-notebook'
};

const PropertyOwners = {
    text: 'Property Owners',
    link: '/propertyowners/list',
    icon: 'icon-people'
};

const Payments = {
    text: 'Payments',
    link: '/payments/list',
    icon: 'icon-star'
};

const Reports = {
    text: 'Reports',
    link: '/reports/list',
    icon: 'icon-star'
};

const headingMain = {
    text: 'Main Navigation',
    heading: true,
    link: null
};

const ChangePassowrd = {
    text: 'Change Password',
    link: '/user/change-password',
    icon: 'icon-lock'
};

export const adminMenu = [
    headingMain,
    Home,
    Tenants,
    Properties,
    AssignedProperties,
    PropertyOwners,
    Payments,
    Reports,
    ChangePassowrd
];

export const userMenu = [
    headingMain,
    Home,
    ChangePassowrd
];
