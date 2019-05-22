﻿using CasinoApplication.Interfaces;
using CasinoApplication.Services;
using CasinoApplication.View;
using CasinoApplication.ViewModel.Commands;
using CasinoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CasinoApplication.ViewModel
{
    public partial class MainViewModel : ViewModel
    {
        private Client selectedClient;

        public Client SelectedClient {
            get { return selectedClient; }
            set {
                selectedClient = value;
                EditClientCommand.RaiseCanExecuteChanged();
                RemoveClientCommand.RaiseCanExecuteChanged();
            }
        }

        private List<Client> clients;

        public List<Client> ClientsList {
            get => clients;
            set {
                clients = value;
                OnPropertyChanged("ClientsCollection");
            }
        }

        public MainViewModel()
        {
            List<Client> clients = new List<Client>
            {
                new Client(0, "Example", "Example")
            };
            ClientsList = clients;

            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());
            ServiceProvider.Instance.Register<IModalDialog, ClientViewDialog>();
        }

        // ------------------------ Commands
        private AddCommand addClientCommand;
        public AddCommand AddClientCommand {
            get {
                if (addClientCommand == null)
                {
                    addClientCommand = new AddCommand(e => AddClient());
                }
                return addClientCommand;
            }
        }

        private EditCommand editClientCommand;
        public EditCommand EditClientCommand {
            get {
                if (editClientCommand == null)
                {
                    editClientCommand = new EditCommand(e => EditClient(SelectedClient), e => SelectedClient != null);
                }
                return editClientCommand;
            }
        }

        private RemoveCommand removeClientCommand;
        public RemoveCommand RemoveClientCommand {
            get {
                if (removeClientCommand == null)
                {
                    removeClientCommand = new RemoveCommand(e => RemoveClient(SelectedClient), e => SelectedClient != null);
                }
                return removeClientCommand;
            }
        }

        // ------------------------ Actions
        public void AddClient()
        {
            ClientViewModel viewModel = new ClientViewModel();

            IModalDialog dialog = ServiceProvider.Instance.Get<IModalDialog>();
            dialog.BindViewModel(viewModel);
            dialog.ShowDialog();
        }

        public void EditClient(Client client)
        {
            MessageBox.Show(client.ToString());
        }

        public void RemoveClient(Client client)
        {
            MessageBox.Show(client.ToString());
        }

    }
}