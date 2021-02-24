using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snusnu.Models
{
    public class WalletTree
    {
        public Wallet Wallet;

        public List<Wallet> Wallets = new List<Wallet>();
        public List<WalletNode> Nodes = new List<WalletNode>();

        public static WalletTree Parse(Wallet wallet)
        {
            WalletTree root = new WalletTree() { Wallet = wallet };
            root.Wallets.Add(wallet);

            List<WalletNode> parentNodes = new List<WalletNode>();
            foreach (var conv in wallet.GetConversions())
            {
                if (!root.Wallets.Contains(conv.Wallet))
                {
                    root.Wallets.Add(conv.Wallet);
                    var node = new WalletNode(root, wallet, conv.Wallet, conv.Market, conv.Side);
                    root.Nodes.Add(node);
                    parentNodes.Add(node);
                }
            }
            do
            {
                List<WalletNode> newParentNodes = new List<WalletNode>();
                foreach (WalletNode parentNode in parentNodes)
                {
                    foreach (var conv in parentNode.Wallet.GetConversions())
                    {
                        if (!root.Wallets.Contains(conv.Wallet))
                        {
                            root.Wallets.Add(conv.Wallet);
                            var node = new WalletNode(root, parentNode.Wallet, conv.Wallet, conv.Market, conv.Side);
                            parentNode.Nodes.Add(node);
                            newParentNodes.Add(node);
                        }
                    }
                }
                parentNodes.Clear();
                parentNodes.AddRange(newParentNodes);
            }
            while (parentNodes.Count > 0);
            return root;
        }

        public override string ToString()
        {
            return Wallet.Code;
        }

        public WalletPath JumpPath(Wallet wallet)
        {
            WalletNode node = Nodes.FirstOrDefault(i => i.Wallet.Code.Equals(wallet.Code));
            if (node != null)
            {
                var walletPath = new WalletPath(this, wallet);
                walletPath.Path.Add((node.Market, node.OrderSide));
                return walletPath;
            }
            else
            {
                foreach (var n in Nodes)
                {
                    WalletPath nodePath = new WalletPath(this, wallet);
                    nodePath.Path.Add((n.Market, n.OrderSide));
                    WalletPath subNodePath = n.JumpPath(wallet, nodePath);
                    if (subNodePath != null) return subNodePath;
                }
            }
            return null;
        }
    }

    public class WalletNode
    {
        public readonly WalletTree Root;
        public readonly Wallet Parent;
        public readonly Wallet Wallet;
        public readonly Market Market;
        public readonly OrderSide OrderSide;
        public readonly List<WalletNode> Nodes = new List<WalletNode>();

        public WalletNode(WalletTree root, Wallet parent, Wallet wallet, Market market, OrderSide orderSide)
        {
            Root = root;
            Parent = parent;
            Wallet = wallet;
            Market = market;
            OrderSide = orderSide;
        }

        public override string ToString()
        {
            return "Market: " + Market.Symbol + ", Wallet: " + Wallet.Code + ", OrderSide: " + (OrderSide == OrderSide.Buy ? "Buy" : "Sell");
        }

        public WalletPath JumpPath(Wallet wallet, WalletPath walletPath)
        {
            WalletNode node = Nodes.FirstOrDefault(i => i.Wallet.Code.Equals(wallet.Code));
            if (node != null)
            {
                walletPath.Path.Add((node.Market, node.OrderSide));
                return walletPath;
            }
            else
            {
                foreach (var n in Nodes)
                {
                    WalletPath nodePath = new WalletPath(Root, wallet);
                    nodePath.Path.AddRange(walletPath.Path);
                    nodePath.Path.Add((n.Market, n.OrderSide));
                    WalletPath subNodePath = n.JumpPath(wallet, nodePath);
                    if (subNodePath != null) return subNodePath;
                }
            }
            return null;
        }
    }

    public class WalletPath
    {
        public readonly WalletTree Source;
        public readonly Wallet Destination;
        public List<(Market Market, OrderSide Side)> Path = new List<(Market Market, OrderSide Side)>();

        public int Jumps
        {
            get
            {
                return Path.Count;
            }
        }

        public WalletPath(WalletTree source, Wallet destination)
        {
            Source = source;
            Destination = destination;
        }

        public override string ToString()
        {
            string path = Path[0].Side == OrderSide.Sell ? Path[0].Market.BaseWallet.Code : Path[0].Market.QuoteWallet.Code;
            for (int i = 0; i < Path.Count; i++)
            {
                path += "=>" + (Path[i].Side == OrderSide.Buy ? Path[i].Market.BaseWallet.Code : Path[i].Market.QuoteWallet.Code);
            }
            return path;
        }

        public decimal ConvertToDestination(decimal value)
        {
            if (value == 0) return 0;
            decimal newValue = value;
            for (int i = 0; i < Path.Count; i++)
            {
                decimal rate = Path[i].Market.Price;
                if (Path[i].Side == OrderSide.Sell)
                {
                    newValue *= rate;
                }
                else
                {
                    newValue /= rate;
                }
            }
            return newValue;
        }

        public decimal ConvertToSource(decimal value)
        {
            if (value == 0) return 0;
            decimal newValue = value;
            for (int i = Path.Count - 1; i >= 0; i--)
            {
                decimal rate = Path[i].Market.Price;
                if (Path[i].Side == OrderSide.Sell)
                {
                    newValue *= rate;
                }
                else
                {
                    newValue /= rate;
                }
            }
            return newValue;
        }
    }
}
