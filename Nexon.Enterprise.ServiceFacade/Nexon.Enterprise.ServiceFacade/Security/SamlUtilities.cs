using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace Nexon.Enterprise.ServiceFacade.Security
{
	internal class SamlUtilities
	{
		public static SamlAssertion CreateSymmetricKeyBasedAssertion(ClaimSet claims, X509SecurityToken signatureToken, X509SecurityToken encryptionToken, BinarySecretSecurityToken proofToken, SecurityAlgorithmSuite algoSuite)
		{
			if (claims == null)
			{
				throw new ArgumentNullException("claims");
			}
			if (claims.Count == 0)
			{
				throw new ArgumentException("Provided ClaimSet must contain at least one claim");
			}
			if (proofToken == null)
			{
				throw new ArgumentNullException("proofToken");
			}
			if (signatureToken == null)
			{
				throw new ArgumentNullException("signatureToken");
			}
			if (encryptionToken == null)
			{
				throw new ArgumentNullException("encryptionToken");
			}
			if (proofToken == null)
			{
				throw new ArgumentNullException("proofToken");
			}
			if (algoSuite == null)
			{
				throw new ArgumentNullException("algoSuite");
			}
			SecurityKey signatureKey = signatureToken.SecurityKeys[0];
			SecurityKeyIdentifierClause securityKeyIdentifierClause = signatureToken.CreateKeyIdentifierClause<X509ThumbprintKeyIdentifierClause>();
			SecurityKeyIdentifier signatureKeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				securityKeyIdentifierClause
			});
			SecurityKey securityKey = encryptionToken.SecurityKeys[0];
			SecurityKeyIdentifierClause securityKeyIdentifierClause2 = encryptionToken.CreateKeyIdentifierClause<X509ThumbprintKeyIdentifierClause>();
			SecurityKeyIdentifier encryptingKeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				securityKeyIdentifierClause2
			});
			byte[] keyBytes = proofToken.GetKeyBytes();
			byte[] encryptedKey = new byte[keyBytes.Length];
			encryptedKey = securityKey.EncryptKey(algoSuite.DefaultAsymmetricKeyWrapAlgorithm, keyBytes);
			SecurityKeyIdentifier proofKeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				new EncryptedKeyIdentifierClause(encryptedKey, algoSuite.DefaultAsymmetricKeyWrapAlgorithm, encryptingKeyIdentifier)
			});
			return SamlUtilities.CreateAssertion(claims, signatureKey, signatureKeyIdentifier, proofKeyIdentifier, algoSuite);
		}

		public static SamlAssertion CreateAsymmetricKeyBasedAssertion(ClaimSet claims, SecurityToken proofToken, SecurityAlgorithmSuite algoSuite)
		{
			if (claims == null)
			{
				throw new ArgumentNullException("claims");
			}
			if (proofToken == null)
			{
				throw new ArgumentNullException("proofToken");
			}
			if (claims.Count == 0)
			{
				throw new ArgumentException("Provided ClaimSet must contain at least one claim");
			}
			SecurityKeyIdentifier securityKeyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				proofToken.CreateKeyIdentifierClause<RsaKeyIdentifierClause>()
			});
			SecurityKey signatureKey = proofToken.SecurityKeys[0];
			SecurityKeyIdentifier signatureKeyIdentifier = securityKeyIdentifier;
			return SamlUtilities.CreateAssertion(claims, signatureKey, signatureKeyIdentifier, securityKeyIdentifier, algoSuite);
		}

		private static SamlAssertion CreateAssertion(ClaimSet claims, SecurityKey signatureKey, SecurityKeyIdentifier signatureKeyIdentifier, SecurityKeyIdentifier proofKeyIdentifier, SecurityAlgorithmSuite algoSuite)
		{
			SamlSubject samlSubject = new SamlSubject(null, null, "Self", new List<string>(1)
			{
				SamlConstants.HolderOfKey
			}, null, proofKeyIdentifier);
			IList<SamlAttribute> list = new List<SamlAttribute>();
			foreach (Claim claim in claims)
			{
				if (typeof(string) == claim.Resource.GetType())
				{
					list.Add(new SamlAttribute(claim));
				}
			}
			SamlAttributeStatement item = new SamlAttributeStatement(samlSubject, list);
			List<SamlStatement> list2 = new List<SamlStatement>();
			list2.Add(item);
			SigningCredentials signingCredentials = new SigningCredentials(signatureKey, algoSuite.DefaultAsymmetricSignatureAlgorithm, algoSuite.DefaultDigestAlgorithm, signatureKeyIdentifier);
			DateTime utcNow = DateTime.UtcNow;
			return new SamlAssertion("_" + Guid.NewGuid().ToString(), "Self", utcNow, new SamlConditions(utcNow, utcNow + new TimeSpan(10, 0, 0)), new SamlAdvice(), list2)
			{
				SigningCredentials = signingCredentials
			};
		}

		public static SecurityToken CreateSymmetricProofToken(int keySize)
		{
			if (keySize < 128 || keySize > 2048)
			{
				throw new ArgumentOutOfRangeException("keySize", "must be in the range 128 to 2048");
			}
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			byte[] array = new byte[keySize / 8];
			rngcryptoServiceProvider.GetNonZeroBytes(array);
			return new BinarySecretSecurityToken(array);
		}

		public static SecurityToken CreateAsymmetricProofToken()
		{
			return new RsaSecurityToken(RSA.Create());
		}

		private SamlUtilities()
		{
		}
	}
}
